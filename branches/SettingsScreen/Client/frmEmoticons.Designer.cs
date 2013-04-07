namespace Client
{
    partial class frmEmoticons
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
            this.panelPicHolder = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // panelPicHolder
            // 
            this.panelPicHolder.AutoScroll = true;
            this.panelPicHolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPicHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPicHolder.Location = new System.Drawing.Point(0, 0);
            this.panelPicHolder.Name = "panelPicHolder";
            this.panelPicHolder.Size = new System.Drawing.Size(292, 93);
            this.panelPicHolder.TabIndex = 0;
            // 
            // frmEmoticons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 93);
            this.Controls.Add(this.panelPicHolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEmoticons";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "frmEmoticons";
            this.Resize += new System.EventHandler(this.frmEmoticons_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelPicHolder;
    }
}