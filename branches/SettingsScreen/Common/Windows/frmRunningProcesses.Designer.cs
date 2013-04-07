namespace Common.Windows
{
    partial class frmRunningProcesses
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
            this.components = new System.ComponentModel.Container();
            this.gridRunningProcesses = new System.Windows.Forms.DataGridView();
            this.contextMnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.killProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridRunningProcesses)).BeginInit();
            this.contextMnu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridRunningProcesses
            // 
            this.gridRunningProcesses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRunningProcesses.ContextMenuStrip = this.contextMnu;
            this.gridRunningProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRunningProcesses.Location = new System.Drawing.Point(0, 0);
            this.gridRunningProcesses.Name = "gridRunningProcesses";
            this.gridRunningProcesses.ReadOnly = true;
            this.gridRunningProcesses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRunningProcesses.Size = new System.Drawing.Size(292, 266);
            this.gridRunningProcesses.TabIndex = 0;
            // 
            // contextMnu
            // 
            this.contextMnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.killProcessToolStripMenuItem});
            this.contextMnu.Name = "contextMnu";
            this.contextMnu.Size = new System.Drawing.Size(138, 26);
            // 
            // killProcessToolStripMenuItem
            // 
            this.killProcessToolStripMenuItem.Name = "killProcessToolStripMenuItem";
            this.killProcessToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.killProcessToolStripMenuItem.Text = "&Kill Process";
            this.killProcessToolStripMenuItem.Click += new System.EventHandler(this.killProcessToolStripMenuItem_Click);
            // 
            // frmRunningProcesses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.gridRunningProcesses);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmRunningProcesses";
            this.Text = "Running Processes";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.gridRunningProcesses)).EndInit();
            this.contextMnu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView gridRunningProcesses;
        private System.Windows.Forms.ContextMenuStrip contextMnu;
        private System.Windows.Forms.ToolStripMenuItem killProcessToolStripMenuItem;

    }
}