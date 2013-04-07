using System;
namespace Client
{
    partial class frmConversation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConversation));
            this.horizontalSplitter = new System.Windows.Forms.SplitContainer();
            this.webDisplay = new System.Windows.Forms.WebBrowser();
            this.contextRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.conversationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timestampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSend = new System.Windows.Forms.RichTextBox();
            this.btnSend = new Telerik.WinControls.UI.RadButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.sendFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.emoticonsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusBottomStatus = new Telerik.WinControls.UI.RadStatusStrip();
            this.lblUserEnteredText = new Telerik.WinControls.UI.RadLabelElement();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.tmrEnteredTextClear = new System.Windows.Forms.Timer(this.components);
            this.tmrAllowEntered = new System.Windows.Forms.Timer(this.components);
            this.extraPluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalSplitter.Panel1.SuspendLayout();
            this.horizontalSplitter.Panel2.SuspendLayout();
            this.horizontalSplitter.SuspendLayout();
            this.contextRightClick.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBottomStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // horizontalSplitter
            // 
            this.horizontalSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.horizontalSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.horizontalSplitter.Location = new System.Drawing.Point(0, 0);
            this.horizontalSplitter.Name = "horizontalSplitter";
            this.horizontalSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // horizontalSplitter.Panel1
            // 
            this.horizontalSplitter.Panel1.Controls.Add(this.webDisplay);
            this.horizontalSplitter.Panel1.Controls.Add(this.menuStrip1);
            // 
            // horizontalSplitter.Panel2
            // 
            this.horizontalSplitter.Panel2.Controls.Add(this.txtSend);
            this.horizontalSplitter.Panel2.Controls.Add(this.btnSend);
            this.horizontalSplitter.Panel2.Controls.Add(this.toolStrip1);
            this.horizontalSplitter.Size = new System.Drawing.Size(306, 382);
            this.horizontalSplitter.SplitterDistance = 256;
            this.horizontalSplitter.TabIndex = 1;
            // 
            // webDisplay
            // 
            this.webDisplay.ContextMenuStrip = this.contextRightClick;
            this.webDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webDisplay.Location = new System.Drawing.Point(0, 24);
            this.webDisplay.MinimumSize = new System.Drawing.Size(20, 20);
            this.webDisplay.Name = "webDisplay";
            this.webDisplay.Size = new System.Drawing.Size(306, 232);
            this.webDisplay.TabIndex = 3;
            this.webDisplay.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webDisplay_Navigating);
            this.webDisplay.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webDisplay_Navigated);
            // 
            // contextRightClick
            // 
            this.contextRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.contextRightClick.Name = "contextRightClick";
            this.contextRightClick.Size = new System.Drawing.Size(125, 48);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conversationToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(306, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // conversationToolStripMenuItem
            // 
            this.conversationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.closeToolStripMenuItem});
            this.conversationToolStripMenuItem.Name = "conversationToolStripMenuItem";
            this.conversationToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.conversationToolStripMenuItem.Text = "&Conversation";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timestampToolStripMenuItem,
            this.alwaysOnTopToolStripMenuItem,
            this.extraPluginsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // timestampToolStripMenuItem
            // 
            this.timestampToolStripMenuItem.Name = "timestampToolStripMenuItem";
            this.timestampToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.timestampToolStripMenuItem.Text = "&Timestamp";
            this.timestampToolStripMenuItem.Click += new System.EventHandler(this.timestampToolStripMenuItem_Click);
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "&Always On Top";
            this.alwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_Click);
            // 
            // txtSend
            // 
            this.txtSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSend.Location = new System.Drawing.Point(0, 25);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(225, 97);
            this.txtSend.TabIndex = 0;
            this.txtSend.Text = "";
            this.txtSend.TextChanged += new System.EventHandler(this.txtSend_TextChanged);
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.Location = new System.Drawing.Point(225, 25);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(81, 97);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "&Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendFileToolStripButton,
            this.emoticonsToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(306, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // sendFileToolStripButton
            // 
            this.sendFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sendFileToolStripButton.Image = global::Client.Properties.Resources.paperclip;
            this.sendFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sendFileToolStripButton.Name = "sendFileToolStripButton";
            this.sendFileToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.sendFileToolStripButton.Text = "Send File";
            this.sendFileToolStripButton.Click += new System.EventHandler(this.sendFileToolStripButton_Click);
            // 
            // emoticonsToolStripButton
            // 
            this.emoticonsToolStripButton.CheckOnClick = true;
            this.emoticonsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.emoticonsToolStripButton.Image = global::Client.Properties.Resources.ewhz;
            this.emoticonsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.emoticonsToolStripButton.Name = "emoticonsToolStripButton";
            this.emoticonsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.emoticonsToolStripButton.Text = "Emoticons";
            this.emoticonsToolStripButton.Click += new System.EventHandler(this.emoticonsToolStripButton_Click);
            // 
            // openDialog
            // 
            this.openDialog.Filter = "All Files (*.*)|*.*";
            this.openDialog.Title = "Select File To Send";
            // 
            // statusBottomStatus
            // 
            this.statusBottomStatus.AutoSize = true;
            this.statusBottomStatus.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.lblUserEnteredText});
            this.statusBottomStatus.LayoutStyle = Telerik.WinControls.UI.RadStatusBarLayoutStyle.Stack;
            this.statusBottomStatus.Location = new System.Drawing.Point(0, 382);
            this.statusBottomStatus.Name = "statusBottomStatus";
            this.statusBottomStatus.Size = new System.Drawing.Size(306, 24);
            this.statusBottomStatus.SizingGrip = true;
            this.statusBottomStatus.TabIndex = 2;
            this.statusBottomStatus.Text = "radStatusStrip1";
            this.statusBottomStatus.ThemeName = "ControlDefault";
            // 
            // lblUserEnteredText
            // 
            this.lblUserEnteredText.Margin = new System.Windows.Forms.Padding(1);
            this.lblUserEnteredText.Name = "lblUserEnteredText";
            this.statusBottomStatus.SetSpring(this.lblUserEnteredText, false);
            // 
            // saveDialog
            // 
            this.saveDialog.Filter = "All Files (*.*)|*.*";
            this.saveDialog.SupportMultiDottedExtensions = true;
            this.saveDialog.Title = "Save Downloaded File As";
            // 
            // tmrEnteredTextClear
            // 
            this.tmrEnteredTextClear.Interval = 15000;
            this.tmrEnteredTextClear.Tick += new System.EventHandler(this.tmrEnteredTextClear_Tick);
            // 
            // tmrAllowEntered
            // 
            this.tmrAllowEntered.Interval = 10000;
            this.tmrAllowEntered.Tick += new System.EventHandler(this.tmrAllowEntered_Tick);
            // 
            // extraPluginsToolStripMenuItem
            // 
            this.extraPluginsToolStripMenuItem.Name = "extraPluginsToolStripMenuItem";
            this.extraPluginsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.extraPluginsToolStripMenuItem.Text = "&Extra Plugins";
            this.extraPluginsToolStripMenuItem.Visible = false;
            // 
            // frmConversation
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 406);
            this.Controls.Add(this.horizontalSplitter);
            this.Controls.Add(this.statusBottomStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmConversation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Shown += new System.EventHandler(this.frmConversation_Shown);
            this.Activated += new System.EventHandler(this.frmConversation_Activated);
            this.Move += new System.EventHandler(this.frmConversation_Move);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConversation_FormClosing);
            this.Resize += new System.EventHandler(this.frmConversation_Resize);
            this.horizontalSplitter.Panel1.ResumeLayout(false);
            this.horizontalSplitter.Panel1.PerformLayout();
            this.horizontalSplitter.Panel2.ResumeLayout(false);
            this.horizontalSplitter.Panel2.PerformLayout();
            this.horizontalSplitter.ResumeLayout(false);
            this.contextRightClick.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBottomStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer horizontalSplitter;
        private System.Windows.Forms.RichTextBox txtSend;
        private System.Windows.Forms.WebBrowser webDisplay;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timestampToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conversationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton sendFileToolStripButton;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.ToolStripButton emoticonsToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private Telerik.WinControls.UI.RadButton btnSend;
        private Telerik.WinControls.UI.RadStatusStrip statusBottomStatus;
        private Telerik.WinControls.UI.RadLabelElement lblUserEnteredText;
        private System.Windows.Forms.ContextMenuStrip contextRightClick;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Timer tmrEnteredTextClear;
        private System.Windows.Forms.Timer tmrAllowEntered;
        private System.Windows.Forms.ToolStripMenuItem extraPluginsToolStripMenuItem;

    }
}