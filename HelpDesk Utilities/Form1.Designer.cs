﻿namespace HelpDesk_Utilities {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("[Dis/En]able WiFi Adaptor");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Add NMU Profile");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("WiFi", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("[Dis/En]able WiMax Adaptor");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("WiMax", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("[Dis/En]able Ethernet Adaptor");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Ethernet", new System.Windows.Forms.TreeNode[] {
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Reset TCP/IP Stack");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Reset Winsock Stack");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("General", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Network Issues", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode5,
            treeNode7,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Malware");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("General/Other");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Run MSPaint");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Sanity Node", new System.Windows.Forms.TreeNode[] {
            treeNode14});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_attemptFixes = new System.Windows.Forms.Button();
            this.richTextBox_logWindow = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(12, 27);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node9";
            treeNode1.Tag = "DisableEnableWiFiAdaptor.ps1";
            treeNode1.Text = "[Dis/En]able WiFi Adaptor";
            treeNode2.Name = "Node1";
            treeNode2.Tag = "AddNMUWiFiProfile.ps1";
            treeNode2.Text = "Add NMU Profile";
            treeNode3.Name = "Node1";
            treeNode3.Text = "WiFi";
            treeNode4.Name = "Node0";
            treeNode4.Tag = "DisableEnableWiMaxAdaptor.ps1";
            treeNode4.Text = "[Dis/En]able WiMax Adaptor";
            treeNode5.Name = "Node2";
            treeNode5.Text = "WiMax";
            treeNode6.Name = "Node0";
            treeNode6.Tag = "DisableEnableEthernetAdaptor.ps1";
            treeNode6.Text = "[Dis/En]able Ethernet Adaptor";
            treeNode7.Name = "Node2";
            treeNode7.Text = "Ethernet";
            treeNode8.Name = "Node8";
            treeNode8.Tag = "ResetTCPIPStack.ps1";
            treeNode8.Text = "Reset TCP/IP Stack";
            treeNode9.Name = "Node7";
            treeNode9.Tag = "ResetWinsockStack.ps1";
            treeNode9.Text = "Reset Winsock Stack";
            treeNode10.Name = "Node0";
            treeNode10.Text = "General";
            treeNode11.Name = "Node0";
            treeNode11.Text = "Network Issues";
            treeNode12.Name = "Node0";
            treeNode12.Text = "Malware";
            treeNode13.Name = "Node1";
            treeNode13.Text = "General/Other";
            treeNode14.Name = "Node6";
            treeNode14.Tag = "MSPaint.ps1";
            treeNode14.Text = "Run MSPaint";
            treeNode15.Name = "Node4";
            treeNode15.Text = "Sanity Node";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode15});
            this.treeView1.Size = new System.Drawing.Size(270, 329);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(686, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Visible = false;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // button_attemptFixes
            // 
            this.button_attemptFixes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_attemptFixes.Location = new System.Drawing.Point(12, 362);
            this.button_attemptFixes.Name = "button_attemptFixes";
            this.button_attemptFixes.Size = new System.Drawing.Size(270, 28);
            this.button_attemptFixes.TabIndex = 2;
            this.button_attemptFixes.Text = "Attempt Selected Fixes";
            this.button_attemptFixes.UseVisualStyleBackColor = true;
            this.button_attemptFixes.Click += new System.EventHandler(this.button_attemptFixesClick);
            // 
            // richTextBox_logWindow
            // 
            this.richTextBox_logWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_logWindow.Location = new System.Drawing.Point(289, 27);
            this.richTextBox_logWindow.Name = "richTextBox_logWindow";
            this.richTextBox_logWindow.Size = new System.Drawing.Size(385, 363);
            this.richTextBox_logWindow.TabIndex = 3;
            this.richTextBox_logWindow.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 402);
            this.Controls.Add(this.richTextBox_logWindow);
            this.Controls.Add(this.button_attemptFixes);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "HelpDesk Utilities";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button button_attemptFixes;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        public System.Windows.Forms.RichTextBox richTextBox_logWindow;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

