using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace HelpDesk_Utilities {
    public partial class Form1 : Form {

        #region Globals
        private bool updatingTreeView;
        BackgroundWorker scriptRunner = new BackgroundWorker();
        private string scriptDirectory =
            @"C:\Users\swillson\Documents\Visual Studio 2012\Projects\HelpDeskUtilities\PSScripts\";
        #endregion

        public Form1() {
            InitializeComponent();

            scriptRunner.DoWork += scriptRunner_DoWork;
            scriptRunner.RunWorkerCompleted += scriptRunner_RunWorkerCompleted;
        }

        #region BackgroundWorker Functions
        /// <summary>
        /// Called whenever scriptRunner_DoWork is completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scriptRunner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            Debug.WriteLine(DateTime.Now.ToLocalTime() + "Mmmmmmwork completedmmmmmmm.");
        }

        /// <summary>
        /// Read in lines of text from specified script file to a string and pass 
        /// it along to RunScript(), which does the actual heavy lifting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scriptRunner_DoWork(object sender, DoWorkEventArgs e) {

            string scriptPath = scriptDirectory + e.Argument.ToString();

            StreamReader myFile =
                new StreamReader(scriptPath);
            string myString = myFile.ReadToEnd();

            myFile.Close();

            string result = RunScript(myString);

            Debug.WriteLine("begin (no text will print, need more understanding of return info): " + result);
        }
        #endregion

        #region AttemptFixes
        /// <summary>
        /// When user clicks "Fix Me" button, spawn a new thread and let it handle
        /// all of the script calling in RunCheckedItems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_attemptFixesClick(object sender, EventArgs e) {

            ParameterizedThreadStart start = new ParameterizedThreadStart(RunCheckedItems);
            Thread thread = new Thread(start);
            thread.Start(treeView1.Nodes);
            
        }

        /// <summary>
        /// Recursively looks through nodes to find all leaf nodes that are checked
        /// and have a value for the "Tag" property. The value should be the script name.
        /// The script associated will get kicked off by BackgroundWorker scriptRunner.
        /// </summary>
        /// <param name="arg">The collection of nodes in the program's main TreeView</param>
        private void RunCheckedItems(Object arg) {

            TreeNodeCollection treeNodeCollection = arg as TreeNodeCollection;

            if (treeNodeCollection.Count == 0)
                return;

            while (scriptRunner.IsBusy) ;

            foreach (TreeNode node in treeNodeCollection) {

                if (node.Checked && node.Tag != null) {
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
            // create Powershell runspace
            Runspace runspace = RunspaceFactory.CreateRunspace();

            // open it
            runspace.Open();

            // create a pipeline and feed it the script text
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(scriptText);

            // add an extra command to transform the script
            // output objects into nicely formatted strings

            // remove this line to get the actual objects
            // that the script returns. For example, the script

            // "Get-Process" returns a collection
            // of System.Diagnostics.Process instances.
            pipeline.Commands.Add("Out-String");

            // execute the script
            Collection<PSObject> results = pipeline.Invoke();

            // close the runspace
            runspace.Close();

            // convert the script result into a single string
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results) {
                stringBuilder.AppendLine(obj.ToString());
            }

            return stringBuilder.ToString();
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
        #endregion
    }
}