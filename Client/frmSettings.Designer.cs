namespace Client
{
    partial class frmSettings
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
            Telerik.WinControls.UI.CarouselBezierPath carouselBezierPath1 = new Telerik.WinControls.UI.CarouselBezierPath();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.toggleStartup = new Telerik.WinControls.UI.RadToggleButtonElement();
            this.toggleConversations = new Telerik.WinControls.UI.RadToggleButtonElement();
            this.toggleSounds = new Telerik.WinControls.UI.RadToggleButtonElement();
            this.carouselSettings = new Telerik.WinControls.UI.RadCarousel();
            this.splitSettings = new System.Windows.Forms.SplitContainer();
            this.settingsTable = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new Telerik.WinControls.UI.RadButton();
            this.btnApply = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.pnlPageHolder = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.carouselSettings)).BeginInit();
            this.splitSettings.Panel1.SuspendLayout();
            this.splitSettings.Panel2.SuspendLayout();
            this.splitSettings.SuspendLayout();
            this.settingsTable.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // toggleStartup
            // 
            this.toggleStartup.CanFocus = true;
            this.toggleStartup.IsChecked = false;
            this.toggleStartup.Name = "toggleStartup";
            this.toggleStartup.StretchVertically = false;
            this.toggleStartup.Text = "Startup";
            // 
            // toggleConversations
            // 
            this.toggleConversations.CanFocus = true;
            this.toggleConversations.IsChecked = false;
            this.toggleConversations.Name = "toggleConversations";
            this.toggleConversations.StretchVertically = false;
            this.toggleConversations.Text = "Conversations";
            // 
            // toggleSounds
            // 
            this.toggleSounds.CanFocus = true;
            this.toggleSounds.IsChecked = false;
            this.toggleSounds.Name = "toggleSounds";
            this.toggleSounds.StretchVertically = false;
            this.toggleSounds.Text = "Sounds";
            // 
            // carouselSettings
            // 
            this.carouselSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.carouselSettings.AutoLoopPauseCondition = Telerik.WinControls.UI.AutoLoopPauseConditions.None;
            carouselBezierPath1.CtrlPoint1 = new Telerik.WinControls.UI.Point3D(90, 70, 70);
            carouselBezierPath1.CtrlPoint2 = new Telerik.WinControls.UI.Point3D(10, 70, 70);
            carouselBezierPath1.FirstPoint = new Telerik.WinControls.UI.Point3D(99, 0, -100);
            carouselBezierPath1.LastPoint = new Telerik.WinControls.UI.Point3D(0, 0, -100);
            carouselBezierPath1.ZScale = 500;
            this.carouselSettings.CarouselPath = carouselBezierPath1;
            this.carouselSettings.Location = new System.Drawing.Point(0, 0);
            this.carouselSettings.Name = "carouselSettings";
            this.carouselSettings.SelectedIndex = 0;
            this.carouselSettings.ShowItemToolTips = false;
            this.carouselSettings.Size = new System.Drawing.Size(439, 115);
            this.carouselSettings.TabIndex = 0;
            this.carouselSettings.ThemeName = "ControlDefault";
            this.carouselSettings.VisibleItemCount = 5;
            this.carouselSettings.SelectedIndexChanged += new System.EventHandler(this.carouselSettings_SelectedIndexChanged);
            // 
            // splitSettings
            // 
            this.splitSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSettings.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitSettings.IsSplitterFixed = true;
            this.splitSettings.Location = new System.Drawing.Point(0, 0);
            this.splitSettings.Name = "splitSettings";
            this.splitSettings.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitSettings.Panel1
            // 
            this.splitSettings.Panel1.Controls.Add(this.carouselSettings);
            // 
            // splitSettings.Panel2
            // 
            this.splitSettings.Panel2.Controls.Add(this.settingsTable);
            this.splitSettings.Size = new System.Drawing.Size(439, 355);
            this.splitSettings.SplitterDistance = 115;
            this.splitSettings.TabIndex = 0;
            // 
            // settingsTable
            // 
            this.settingsTable.ColumnCount = 1;
            this.settingsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.settingsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.settingsTable.Controls.Add(this.panel1, 0, 1);
            this.settingsTable.Controls.Add(this.pnlPageHolder, 0, 0);
            this.settingsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTable.Location = new System.Drawing.Point(0, 0);
            this.settingsTable.Name = "settingsTable";
            this.settingsTable.RowCount = 2;
            this.settingsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.settingsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.76471F));
            this.settingsTable.Size = new System.Drawing.Size(439, 236);
            this.settingsTable.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 197);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(433, 36);
            this.panel1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(187, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(268, 7);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "&Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(349, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlPageHolder
            // 
            this.pnlPageHolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPageHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPageHolder.Location = new System.Drawing.Point(3, 3);
            this.pnlPageHolder.Name = "pnlPageHolder";
            this.pnlPageHolder.Size = new System.Drawing.Size(433, 188);
            this.pnlPageHolder.TabIndex = 1;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 355);
            this.Controls.Add(this.splitSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instant Messenger Settings";
            this.Shown += new System.EventHandler(this.frmSettings_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.carouselSettings)).EndInit();
            this.splitSettings.Panel1.ResumeLayout(false);
            this.splitSettings.Panel2.ResumeLayout(false);
            this.splitSettings.ResumeLayout(false);
            this.settingsTable.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadToggleButtonElement toggleStartup;
        private Telerik.WinControls.UI.RadToggleButtonElement toggleConversations;
        private Telerik.WinControls.UI.RadToggleButtonElement toggleSounds;
        private Telerik.WinControls.UI.RadCarousel carouselSettings;
        private System.Windows.Forms.SplitContainer splitSettings;
        private System.Windows.Forms.TableLayoutPanel settingsTable;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadButton btnOK;
        private Telerik.WinControls.UI.RadButton btnApply;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private System.Windows.Forms.Panel pnlPageHolder;
    }
}