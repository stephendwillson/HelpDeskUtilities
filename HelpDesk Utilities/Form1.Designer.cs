namespace HelpDesk_Utilities {
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Reset Winsock Stack");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Reset TCP/IP Stack");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Disable/Enable WiFi Adapter");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("WiFi", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("WiMax");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Ethernet");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Network Issues", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Run MSPaint");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Sanity Node", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("TestChildNode");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("TestRootNode", new System.Windows.Forms.TreeNode[] {
            treeNode10});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_attemptFixes = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
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
            treeNode1.Name = "Node7";
            treeNode1.Tag = "ResetWinsockStack.ps1";
            treeNode1.Text = "Reset Winsock Stack";
            treeNode2.Name = "Node8";
            treeNode2.Text = "Reset TCP/IP Stack";
            treeNode3.Name = "Node9";
            treeNode3.Text = "Disable/Enable WiFi Adapter";
            treeNode4.Name = "Node1";
            treeNode4.Text = "WiFi";
            treeNode5.Name = "Node2";
            treeNode5.Text = "WiMax";
            treeNode6.Name = "Node2";
            treeNode6.Text = "Ethernet";
            treeNode7.Name = "Node0";
            treeNode7.Text = "Network Issues";
            treeNode8.Name = "Node6";
            treeNode8.Tag = "MSPaint.ps1";
            treeNode8.Text = "Run MSPaint";
            treeNode9.Name = "Node4";
            treeNode9.Text = "Sanity Node";
            treeNode10.Name = "Node5";
            treeNode10.Text = "TestChildNode";
            treeNode11.Name = "Node3";
            treeNode11.Text = "TestRootNode";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode9,
            treeNode11});
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
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
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
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(289, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(385, 363);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "Log of actions will go here";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 402);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button_attemptFixes);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "HelpDesk Utilities";
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
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

