using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HelpDesk_Utilities {
    public partial class Form1 : Form {

        #region Globals

        private readonly object TasksLock = new object();
        private bool updatingTreeView = false, isScriptRunning = false;
        BackgroundWorker scriptRunner = new BackgroundWorker();
        public delegate void InvokeDelegate();

        //encapsulating fields for thread-safe goodness
        private int taskCount = 0, tasksRun = 0;
        public int TaskCount {
            get { lock (TasksLock) { return taskCount; } }
            set { lock (TasksLock) { taskCount = value; } }
        }

        public int TasksRun {
            get { lock (TasksLock) { return tasksRun; } }
            set { lock (TasksLock) { tasksRun = value; } }
        }

        private string scriptDirectory =
            Application.StartupPath + @"\PSScripts\"; //USE THIS FOR RELEASE BUILDS
            //@"C:\Users\aramirez\Documents\GitHub\HelpDeskUtilities\PSScripts\"; //DEBUG BUILD
            //@"C:\Users\swillson\Documents\Visual Studio 2012\Projects\HelpDeskUtilities\PSScripts\"; //DEBUG BUILD

        #endregion
       
        public Form1() {
            InitializeComponent();

            Logger log = new Logger(this);
            AppStats appStats = new AppStats(label_memoryUsage);
            
            scriptRunner.DoWork += scriptRunner_DoWork;
            scriptRunner.RunWorkerCompleted += scriptRunner_RunWorkerCompleted;
        }

        #region BackgroundWorker Functions
        /// <summary>
        /// Read in lines of text from specified script file to a string and pass 
        /// it along to RunScript(), which does the actual heavy lifting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">e.Argument.ToString will be the script name (script.ps1)</param>
        private void scriptRunner_DoWork(object sender, DoWorkEventArgs e) {

            string scriptPath = "";
            try {
                scriptPath = scriptDirectory + e.Argument.ToString();

                StreamReader myFile =
                    new StreamReader(scriptPath);
                string myString = myFile.ReadToEnd();

                myFile.Close();

                string result = RunScript(myString);

                if (!String.IsNullOrWhiteSpace(result))
                    Logger.Log(result,Color.Black,false);

                e.Result = e.Argument.ToString();
            }
            catch (DirectoryNotFoundException ex) {
                e.Result = ex;
                return;
            }
        }
        /// <summary>
        /// Called whenever scriptRunner_DoWork is completed. If all scripts are finished, this will
        /// log a notification in the RichTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">e.Result.ToString() will be the Tag associated with any given script, unless an exception is caught in doWork</param>
        private void scriptRunner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

            DirectoryNotFoundException error = e.Result as DirectoryNotFoundException;

            if (e.Result is DirectoryNotFoundException) {
                Logger.Log("Script did not run successfully: " + error.Message + "\n", Color.Red, true);
            }
            else {
                Logger.Log("Finished script " + e.Result.ToString() + ".\n", Color.Black, true);
            }

            TasksRun++;

            if (TaskCount != 0 && TasksRun == TaskCount) {
                    Logger.Log("Done with fixes!\n", Color.Black, true);
                    TaskCount = TasksRun = 0;
                    button_attemptFixes.BeginInvoke(new InvokeDelegate(InvokeMethod));
            }
        }
        #endregion

        #region AttemptFixes
        /// <summary>
        /// When user clicks "Fix Me" button, spawn a new thread and let it handle the script calling in RunCheckedItems.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_attemptFixesClick(object sender, EventArgs e) {

            button_attemptFixes.Enabled = false;
            button_clearAllCheckboxes.Enabled = false;

            ParameterizedThreadStart start = new ParameterizedThreadStart(RunCheckedItems);
            Thread thread = new Thread(start);
            thread.Start(treeView1.Nodes);
        }

        public void InvokeMethod() {

            button_attemptFixes.Enabled = true;
            button_clearAllCheckboxes.Enabled = true;
        }

        /// <summary>
        /// Recursively looks through nodes to find all leaf nodes that are checked
        /// and have a value for the "Tag" property. The value should be the script name.
        /// The script associated will get kicked off by BackgroundWorker scriptRunner.
        /// The total number of tasks to be run is also calculated here.
        /// </summary>
        /// <param name="arg">The collection of nodes in the program's main TreeView</param>
        private void RunCheckedItems(Object arg) {

            TreeNodeCollection treeNodeCollection = arg as TreeNodeCollection;

            if (treeNodeCollection.Count == 0)
                return;

            foreach (TreeNode node in treeNodeCollection) {
                if (node.Checked && node.Tag != null)
                    TaskCount++;
            }
                        
            foreach (TreeNode node in treeNodeCollection) {

                if (node.Checked && node.Tag != null) {
                    while (scriptRunner.IsBusy) ;
                    Logger.Log("Beginning script " + node.Tag + ".", Color.Black, true);
                    scriptRunner.RunWorkerAsync(node.Tag);
                }

                RunCheckedItems(node.Nodes);
            }
        }

        /// <summary>
        /// Pipe all the text from a script (scriptText) to the Powershell runspace and run that shiz.
        /// </summary>
        /// <param name="scriptText">The full text of the script to be run</param>
        /// <returns>Returns any relevant "finish" data from script, as string for now--to be changed</returns>
        private string RunScript(string scriptText) {

            try {
                // create Powershell runspace
                Runspace runspace = RunspaceFactory.CreateRunspace();

                isScriptRunning = true;

                // open it
                runspace.Open();

                // create a pipeline and feed it the script text
                Pipeline pipeline = runspace.CreatePipeline();
                pipeline.Commands.AddScript(scriptText);

                // add an extra command to transform the script
                // output objects into nicely formatted strings
                pipeline.Commands.Add("Out-String");

                // execute the script
                Collection<PSObject> results = pipeline.Invoke();

                // close the runspace
                runspace.Close();

                if (pipeline.Error.Count > 0) {
                    Logger.Log("Error: " + pipeline.Error.Read(), Color.Red, true);
                }

                isScriptRunning = false;

                // convert the script result into a single string
                StringBuilder stringBuilder = new StringBuilder();
                foreach (PSObject obj in results) {
                    stringBuilder.AppendLine(obj.ToString());
                }

                return stringBuilder.ToString();
            }
            catch (Exception ex) {
                isScriptRunning = false;
                return "Exception caught while invoking Powershell script: " + ex.Message.ToString() + "\n";
            }
        }
        #endregion

        //need to clarify more rules for checkbox behavior, still a bit wonky - try clicking fast, you'll see
        #region TreeView Checkbox Behavior
        /// <summary>
        /// Check all child nodes if a parent node is checked.
        /// </summary>
        /// <param name="node">Node being checked</param>
        /// <param name="isChecked">Node's current checked status</param>
        private void CheckChildren_ParentSelected(TreeNode node, Boolean isChecked) {

            foreach (TreeNode item in node.Nodes) {
                item.Checked = isChecked;

                if (item.Nodes.Count > 0) {
                    this.CheckChildren_ParentSelected(item, isChecked);
                }
            }
        }

        /// <summary>
        /// Check all parent nodes if a child node is checked.
        /// </summary>
        /// <param name="node">Node being checked</param>
        /// <param name="isChecked">Node's current checked status</param>
        private void SelectParents(TreeNode node, Boolean isChecked) {

            if (node.Parent != null) {
                node.Parent.Checked = isChecked;
                SelectParents(node.Parent, isChecked);
            }
        }

        /// <summary>
        /// Updates TreeView if any node is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e) {

            if(updatingTreeView) 
                return;
            updatingTreeView = true;
            CheckChildren_ParentSelected(e.Node, e.Node.Checked);
            SelectParents(e.Node, e.Node.Checked);
            updatingTreeView = false;
        }

        /// <summary>
        /// On click, clears all checkboxes in TreeView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e) {
            
            foreach (TreeNode node in treeView1.Nodes) {
                node.Checked = false;
            }
        }
        #endregion

        #region Exit Behavior
        //Clean up this method
        /// <summary>
        /// This will run when Exit is selected from the File menu. It will prompt for confirmation if a script is currently running.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Pass in FormClosingEventArgs if necessary.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {

            FormClosingEventArgs eve = null;
            if(e != null)
                eve = e as FormClosingEventArgs;

            if (isScriptRunning) {
                DialogResult quitResult = MessageBox.Show("A fix is currently running. Are you sure you want to quit?", "Oops!", MessageBoxButtons.YesNo);

                if (quitResult == DialogResult.No) {
                    if(eve != null)
                        eve.Cancel = true;
                    return;
                }
            }
            Environment.Exit(0);
        }

        /// <summary>
        /// Function to run when application is closed. Will prompt for confirmation if script is running. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {

            exitToolStripMenuItem_Click(sender, e);
        }
        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {

        }
    }
}