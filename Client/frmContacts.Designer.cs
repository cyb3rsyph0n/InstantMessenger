namespace Client
{
    partial class frmContacts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContacts));
            this.mnuMainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.awayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outToLunchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.missedConversationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentConversationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extraPluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrKeepAlive = new System.Windows.Forms.Timer(this.components);
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMnuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMnuContacts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendIMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getRunningProcessesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getScreenShotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactImages = new System.Windows.Forms.ImageList(this.components);
            this.treeContacts = new Telerik.WinControls.UI.RadTreeView();
            this.mnuMainMenu.SuspendLayout();
            this.contextMnuTray.SuspendLayout();
            this.contextMnuContacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeContacts)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMainMenu
            // 
            this.mnuMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.statusToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnuMainMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMainMenu.Name = "mnuMainMenu";
            this.mnuMainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMainMenu.Size = new System.Drawing.Size(208, 24);
            this.mnuMainMenu.TabIndex = 4;
            this.mnuMainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusToolStripMenuItem
            // 
            this.statusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activeToolStripMenuItem,
            this.awayToolStripMenuItem,
            this.outToLunchToolStripMenuItem});
            this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            this.statusToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.statusToolStripMenuItem.Text = "&Status";
            // 
            // activeToolStripMenuItem
            // 
            this.activeToolStripMenuItem.Checked = true;
            this.activeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activeToolStripMenuItem.Name = "activeToolStripMenuItem";
            this.activeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.activeToolStripMenuItem.Text = "A&ctive";
            this.activeToolStripMenuItem.Click += new System.EventHandler(this.activeToolStripMenuItem_Click);
            // 
            // awayToolStripMenuItem
            // 
            this.awayToolStripMenuItem.Name = "awayToolStripMenuItem";
            this.awayToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.awayToolStripMenuItem.Text = "&Away";
            this.awayToolStripMenuItem.Click += new System.EventHandler(this.awayToolStripMenuItem_Click);
            // 
            // outToLunchToolStripMenuItem
            // 
            this.outToLunchToolStripMenuItem.Name = "outToLunchToolStripMenuItem";
            this.outToLunchToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.outToLunchToolStripMenuItem.Text = "&Out to Lunch";
            this.outToLunchToolStripMenuItem.Click += new System.EventHandler(this.outToLunchToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.missedConversationsToolStripMenuItem,
            this.recentConversationsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.preferencesToolStripMenuItem,
            this.extraPluginsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // missedConversationsToolStripMenuItem
            // 
            this.missedConversationsToolStripMenuItem.Name = "missedConversationsToolStripMenuItem";
            this.missedConversationsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.missedConversationsToolStripMenuItem.Text = "&Missed Conversations";
            this.missedConversationsToolStripMenuItem.Click += new System.EventHandler(this.missedConversationsToolStripMenuItem_Click);
            // 
            // recentConversationsToolStripMenuItem
            // 
            this.recentConversationsToolStripMenuItem.Name = "recentConversationsToolStripMenuItem";
            this.recentConversationsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.recentConversationsToolStripMenuItem.Text = "&Recent Conversations";
            this.recentConversationsToolStripMenuItem.Click += new System.EventHandler(this.recentConversationsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(186, 6);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.preferencesToolStripMenuItem.Text = "&Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // extraPluginsToolStripMenuItem
            // 
            this.extraPluginsToolStripMenuItem.Name = "extraPluginsToolStripMenuItem";
            this.extraPluginsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.extraPluginsToolStripMenuItem.Text = "&Extra Plugins";
            this.extraPluginsToolStripMenuItem.Visible = false;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tmrKeepAlive
            // 
            this.tmrKeepAlive.Interval = 60000;
            this.tmrKeepAlive.Tick += new System.EventHandler(this.tmrKeepAlive_Tick);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.contextMnuTray;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Instant Messenger";
            this.trayIcon.Visible = true;
            this.trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseDoubleClick);
            // 
            // contextMnuTray
            // 
            this.contextMnuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem1});
            this.contextMnuTray.Name = "contextMnuTray";
            this.contextMnuTray.Size = new System.Drawing.Size(114, 54);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.restoreToolStripMenuItem.Text = "&Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(110, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.exitToolStripMenuItem1.Text = "&Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // contextMnuContacts
            // 
            this.contextMnuContacts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendIMToolStripMenuItem,
            this.toolStripMenuItem2,
            this.extrasToolStripMenuItem});
            this.contextMnuContacts.Name = "contextMnu";
            this.contextMnuContacts.Size = new System.Drawing.Size(153, 76);
            // 
            // sendIMToolStripMenuItem
            // 
            this.sendIMToolStripMenuItem.Name = "sendIMToolStripMenuItem";
            this.sendIMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sendIMToolStripMenuItem.Text = "&Send IM";
            this.sendIMToolStripMenuItem.Click += new System.EventHandler(this.sendIMToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            this.toolStripMenuItem2.Visible = false;
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getRunningProcessesToolStripMenuItem,
            this.getScreenShotToolStripMenuItem});
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            this.extrasToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.extrasToolStripMenuItem.Text = "&Extras";
            // 
            // getRunningProcessesToolStripMenuItem
            // 
            this.getRunningProcessesToolStripMenuItem.Name = "getRunningProcessesToolStripMenuItem";
            this.getRunningProcessesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.getRunningProcessesToolStripMenuItem.Text = "Get &Running Processes";
            this.getRunningProcessesToolStripMenuItem.Click += new System.EventHandler(this.getRunningProcessesToolStripMenuItem_Click);
            // 
            // getScreenShotToolStripMenuItem
            // 
            this.getScreenShotToolStripMenuItem.Name = "getScreenShotToolStripMenuItem";
            this.getScreenShotToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.getScreenShotToolStripMenuItem.Text = "Get &Screen Shot";
            this.getScreenShotToolStripMenuItem.Click += new System.EventHandler(this.getScreenShotToolStripMenuItem_Click);
            // 
            // contactImages
            // 
            this.contactImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("contactImages.ImageStream")));
            this.contactImages.TransparentColor = System.Drawing.Color.Transparent;
            this.contactImages.Images.SetKeyName(0, "User");
            // 
            // treeContacts
            // 
            this.treeContacts.AllowDragDropBetweenTreeViews = false;
            this.treeContacts.AllowMultiselect = true;
            this.treeContacts.BackColor = System.Drawing.Color.White;
            this.treeContacts.ContextMenuStrip = this.contextMnuContacts;
            this.treeContacts.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeContacts.Font = new System.Drawing.Font("Tahoma", 8.6F);
            this.treeContacts.ForeColor = System.Drawing.SystemColors.ControlText;
            this.treeContacts.Location = new System.Drawing.Point(0, 24);
            this.treeContacts.Margin = new System.Windows.Forms.Padding(0);
            this.treeContacts.Name = "treeContacts";
            this.treeContacts.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.treeContacts.ShowLines = true;
            this.treeContacts.Size = new System.Drawing.Size(208, 422);
            this.treeContacts.TabIndex = 8;
            this.treeContacts.ThemeName = "ControlDefault";
            this.treeContacts.DoubleClick += new System.EventHandler(this.treeContacts_DoubleClick);
            // 
            // frmContacts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 446);
            this.Controls.Add(this.treeContacts);
            this.Controls.Add(this.mnuMainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMainMenu;
            this.MaximizeBox = false;
            this.Name = "frmContacts";
            this.Tag = "Contacts";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmContacts_FormClosing);
            this.Resize += new System.EventHandler(this.frmContacts_Resize);
            this.mnuMainMenu.ResumeLayout(false);
            this.mnuMainMenu.PerformLayout();
            this.contextMnuTray.ResumeLayout(false);
            this.contextMnuContacts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeContacts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMainMenu;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentConversationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer tmrKeepAlive;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip contextMnuContacts;
        private System.Windows.Forms.ToolStripMenuItem sendIMToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMnuTray;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem awayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activeToolStripMenuItem;
        private System.Windows.Forms.ImageList contactImages;
        private System.Windows.Forms.ToolStripMenuItem outToLunchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getRunningProcessesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getScreenShotToolStripMenuItem;
        private Telerik.WinControls.UI.RadTreeView treeContacts;
        private System.Windows.Forms.ToolStripMenuItem missedConversationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extraPluginsToolStripMenuItem;
    }
}

