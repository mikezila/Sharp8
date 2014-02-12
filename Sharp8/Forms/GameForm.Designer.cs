namespace Sharp8
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeRomMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.resetEmulatorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quadDisplayScaleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sexDisplayScaleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.octoSizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debuggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.pauseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSharp8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.x10SizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.emulationToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(523, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeRomMenuItem,
            this.toolStripSeparator2,
            this.resetEmulatorMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeRomMenuItem
            // 
            this.closeRomMenuItem.Enabled = false;
            this.closeRomMenuItem.Name = "closeRomMenuItem";
            this.closeRomMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeRomMenuItem.Text = "Close";
            this.closeRomMenuItem.Click += new System.EventHandler(this.closeRomMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // resetEmulatorMenuItem
            // 
            this.resetEmulatorMenuItem.Enabled = false;
            this.resetEmulatorMenuItem.Name = "resetEmulatorMenuItem";
            this.resetEmulatorMenuItem.Size = new System.Drawing.Size(152, 22);
            this.resetEmulatorMenuItem.Text = "Reset";
            this.resetEmulatorMenuItem.Click += new System.EventHandler(this.resetEmulatorMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // emulationToolStripMenuItem
            // 
            this.emulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayScaleToolStripMenuItem,
            this.toolStripSeparator3,
            this.pauseMenuItem,
            this.stepToolStripMenuItem,
            this.toolStripSeparator4,
            this.debuggerToolStripMenuItem});
            this.emulationToolStripMenuItem.Name = "emulationToolStripMenuItem";
            this.emulationToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.emulationToolStripMenuItem.Text = "Emulation";
            // 
            // displayScaleToolStripMenuItem
            // 
            this.displayScaleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quadDisplayScaleMenuItem,
            this.sexDisplayScaleMenuItem,
            this.octoSizeMenuItem,
            this.x10SizeMenuItem});
            this.displayScaleToolStripMenuItem.Name = "displayScaleToolStripMenuItem";
            this.displayScaleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.displayScaleToolStripMenuItem.Text = "Display Scale";
            // 
            // quadDisplayScaleMenuItem
            // 
            this.quadDisplayScaleMenuItem.Name = "quadDisplayScaleMenuItem";
            this.quadDisplayScaleMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quadDisplayScaleMenuItem.Text = "4x";
            this.quadDisplayScaleMenuItem.Click += new System.EventHandler(this.quadDisplayScaleMenuItem_Click);
            // 
            // sexDisplayScaleMenuItem
            // 
            this.sexDisplayScaleMenuItem.Name = "sexDisplayScaleMenuItem";
            this.sexDisplayScaleMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sexDisplayScaleMenuItem.Text = "6x";
            this.sexDisplayScaleMenuItem.Click += new System.EventHandler(this.sexDisplayScaleMenuItem_Click);
            // 
            // octoSizeMenuItem
            // 
            this.octoSizeMenuItem.Name = "octoSizeMenuItem";
            this.octoSizeMenuItem.Size = new System.Drawing.Size(152, 22);
            this.octoSizeMenuItem.Text = "8x";
            this.octoSizeMenuItem.Click += new System.EventHandler(this.octoSizeMenuItem_Click);
            // 
            // debuggerToolStripMenuItem
            // 
            this.debuggerToolStripMenuItem.Name = "debuggerToolStripMenuItem";
            this.debuggerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.debuggerToolStripMenuItem.Text = "Debugger";
            this.debuggerToolStripMenuItem.Click += new System.EventHandler(this.debuggerToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // pauseMenuItem
            // 
            this.pauseMenuItem.Name = "pauseMenuItem";
            this.pauseMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pauseMenuItem.Text = "Pause";
            this.pauseMenuItem.Click += new System.EventHandler(this.pauseMenuItem_Click);
            // 
            // stepToolStripMenuItem
            // 
            this.stepToolStripMenuItem.Name = "stepToolStripMenuItem";
            this.stepToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stepToolStripMenuItem.Text = "Step";
            this.stepToolStripMenuItem.Click += new System.EventHandler(this.stepMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutSharp8ToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutSharp8ToolStripMenuItem
            // 
            this.aboutSharp8ToolStripMenuItem.Name = "aboutSharp8ToolStripMenuItem";
            this.aboutSharp8ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.aboutSharp8ToolStripMenuItem.Text = "About Sharp8";
            this.aboutSharp8ToolStripMenuItem.Click += new System.EventHandler(this.aboutSharp8ToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // x10SizeMenuItem
            // 
            this.x10SizeMenuItem.Name = "x10SizeMenuItem";
            this.x10SizeMenuItem.Size = new System.Drawing.Size(152, 22);
            this.x10SizeMenuItem.Text = "10x";
            this.x10SizeMenuItem.Click += new System.EventHandler(this.x10SizeMenuItem_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 382);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameForm";
            this.Text = "Sharp8";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quadDisplayScaleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sexDisplayScaleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debuggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSharp8ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeRomMenuItem;
        private System.Windows.Forms.ToolStripMenuItem octoSizeMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem resetEmulatorMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem pauseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem x10SizeMenuItem;
    }
}

