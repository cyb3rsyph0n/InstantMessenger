namespace Client.SettingsControls
{
    partial class propertyPageConversations
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
            this.chkTimeStamps = new System.Windows.Forms.CheckBox();
            this.numConversationDays = new System.Windows.Forms.NumericUpDown();
            this.chkMissedConversations = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numConversationDays)).BeginInit();
            this.SuspendLayout();
            // 
            // chkTimeStamps
            // 
            this.chkTimeStamps.AutoSize = true;
            this.chkTimeStamps.Checked = true;
            this.chkTimeStamps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTimeStamps.Location = new System.Drawing.Point(35, 50);
            this.chkTimeStamps.Name = "chkTimeStamps";
            this.chkTimeStamps.Size = new System.Drawing.Size(167, 17);
            this.chkTimeStamps.TabIndex = 3;
            this.chkTimeStamps.Text = "Display Message TimeStamps";
            this.chkTimeStamps.UseVisualStyleBackColor = true;
            // 
            // numConversationDays
            // 
            this.numConversationDays.Location = new System.Drawing.Point(275, 119);
            this.numConversationDays.Name = "numConversationDays";
            this.numConversationDays.Size = new System.Drawing.Size(34, 20);
            this.numConversationDays.TabIndex = 5;
            this.numConversationDays.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // chkMissedConversations
            // 
            this.chkMissedConversations.AutoSize = true;
            this.chkMissedConversations.Checked = true;
            this.chkMissedConversations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMissedConversations.Location = new System.Drawing.Point(35, 83);
            this.chkMissedConversations.Name = "chkMissedConversations";
            this.chkMissedConversations.Size = new System.Drawing.Size(225, 17);
            this.chkMissedConversations.TabIndex = 6;
            this.chkMissedConversations.Text = "Checked for missed conversations at login";
            this.chkMissedConversations.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Number of days to display a recent conversation";
            // 
            // propertyPageConversations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkMissedConversations);
            this.Controls.Add(this.numConversationDays);
            this.Controls.Add(this.chkTimeStamps);
            this.Name = "propertyPageConversations";
            this.Size = new System.Drawing.Size(433, 188);
            ((System.ComponentModel.ISupportInitialize)(this.numConversationDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkTimeStamps;
        private System.Windows.Forms.NumericUpDown numConversationDays;
        private System.Windows.Forms.CheckBox chkMissedConversations;
        private System.Windows.Forms.Label label1;

    }
}
