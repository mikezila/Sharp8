namespace Sharp8
{
    partial class Debugger
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
            this.debuggerTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // debuggerTextBox
            // 
            this.debuggerTextBox.Location = new System.Drawing.Point(12, 12);
            this.debuggerTextBox.Name = "debuggerTextBox";
            this.debuggerTextBox.ReadOnly = true;
            this.debuggerTextBox.Size = new System.Drawing.Size(243, 184);
            this.debuggerTextBox.TabIndex = 0;
            this.debuggerTextBox.Text = "";
            // 
            // Debugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 208);
            this.Controls.Add(this.debuggerTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Debugger";
            this.Text = "Debugger";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox debuggerTextBox;
    }
}