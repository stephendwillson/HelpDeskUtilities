﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private bool isScriptRunning = false;
        private string scriptDirectory =
            @"C:\Users\aramirez\Documents\GitHub\HelpDeskUtilities\PSScripts\";
           // @"C:\Users\swillson\Documents\Visual Studio 2012\Projects\HelpDeskUtilities\PSScripts\";
        #endregion
       
        public Form1() {
            InitializeComponent();

            Logger log = new Logger(this);
            
            scriptRunner.DoWork += scriptRunner_DoWork;
            scriptRunner.RunWorkerCompleted += scriptRunner_RunWorkerCompleted;
        }

        #region BackgroundWorker Functions
        /// <summary>
        /// Called whenever scriptRunner_DoWork is completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">e.Result.ToString() will be the Tag associated with any given script</param>
        private void scriptRunner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            Logger.Log("Finished script " + e.Result.ToString() + ".");
        }

        /// <summary>
        /// Read in lines of text from specified script file to a string and pass 
        /// it along to RunScript(), which does the actual heavy lifting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">e.Argument.ToString will be the script name (script.ps1)</param>
        private void scriptRunner_DoWork(object sender, DoWorkEventArgs e) {

            string scriptPath = scriptDirectory + e.Argument.ToString();

            StreamReader myFile =
                new StreamReader(scriptPath);
            string myString = myFile.ReadToEnd();

            myFile.Close();

            string result = RunScript(myString);

            if (!String.IsNullOrEmpty(result))
                Logger.Log(result);

            e.Result = e.Argument.ToString();
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
                        
            foreach (TreeNode node in treeNodeCollection) {

                if (node.Checked && node.Tag != null) {
                    while (scriptRunner.IsBusy) ;
                    Logger.Log("Beginning script " + node.Tag + ".");
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
                    Logger.Log("Error: " + pipeline.Error.Read());
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
                return "Exception caught while invoking Powershell script: " + ex.Message.ToString();
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