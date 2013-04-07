namespace WindowPositionSaver
{
    partial class propertyPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkContactsWindow = new System.Windows.Forms.CheckBox();
            this.chkConversationWindows = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkContactsWindow
            // 
            this.chkContactsWindow.AutoSize = true;
            this.chkContactsWindow.Location = new System.Drawing.Point(32, 57);
            this.chkContactsWindow.Name = "chkContactsWindow";
            this.chkContactsWindow.Size = new System.Drawing.Size(183, 17);
            this.chkContactsWindow.TabIndex = 1;
            this.chkContactsWindow.Text = "Save Contacts Window Positions";
            this.chkContactsWindow.UseVisualStyleBackColor = true;
            // 
            // chkConversationWindows
            // 
            this.chkConversationWindows.AutoSize = true;
            this.chkConversationWindows.Location = new System.Drawing.Point(32, 109);
            this.chkConversationWindows.Name = "chkConversationWindows";
            this.chkConversationWindows.Size = new System.Drawing.Size(203, 17);
            this.chkConversationWindows.TabIndex = 2;
            this.chkConversationWindows.Text = "Save Conversation Window Positions";
            this.chkConversationWindows.UseVisualStyleBackColor = true;
            // 
            // propertyPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkConversationWindows);
            this.Controls.Add(this.chkContactsWindow);
            this.Name = "propertyPage";
            this.Size = new System.Drawing.Size(433, 188);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkContactsWindow;
        private System.Windows.Forms.CheckBox chkConversationWindows;

    }
}
