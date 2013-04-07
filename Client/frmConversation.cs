using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Interfaces;
using Common.Messages;
using Common.Connections;
using System.IO;
using Common.Sounds;
using System.Text.RegularExpressions;
using Common.Config;
using System.Reflection;

namespace Client
{
    public partial class frmConversation : Form, IConversationWindow
    {
        public Connection ThisConnection = null;
        private HtmlElementEventHandler ClickHandler;
        private HtmlElementEventHandler MouseDownHandler;
        private bool LinkClicked = false;
        private List<Dictionary<string, string>> Emoticons = null;
        private frmEmoticons EmoticonsDisplay;
        private bool FirstMessage = true;
        private bool HasUpdateBeenSent = false;

        private string DownloadingFileID = null;
        private string DownloadingFileName = null;

        private Dictionary<string, IConversationPlugin> Plugins = new Dictionary<string, IConversationPlugin>();

        public string ConversationID
        {
            get
            {
                return (string)this.Tag;
            }
            set
            {
                this.Tag = value;
            }
        }

        public frmConversation(Connection ThisConnection, string ConversationID)
        {
            InitializeComponent();
            this.ThisConnection = ThisConnection;
            this.ConversationID = ConversationID;
            ClickHandler = new HtmlElementEventHandler(frmConversation_Click);
            MouseDownHandler = new HtmlElementEventHandler(frmConversation_MouseDown);
            ClearMessages();

            //LOAD THE EMOTICONS FROM THE CONFIG FILE
            Emoticons = ConfigWrapper.GetSetting(Path.Combine(Application.StartupPath, "Images\\Emoticons\\emoticons.xml"), "icon");

            //CREATE A NEW EMOTICONS DISPLAY
            EmoticonsDisplay = new frmEmoticons(this, Emoticons);
            EmoticonsDisplay.EmoticonClicked += new frmEmoticons.EmoticonClickedDelegate(EmoticonsDisplay_EmoticonClicked);

            //CREATE PLUGIN LIST
            CreatePluginList();

            //DETERMINE IF THE TIMESTAMPS SHOULD BE ENABLED BY DEFAULT OR NOT
            if (ConfigWrapper.GetSetting("ConversationSettings").FirstOrDefault() != null)
            {
                timestampToolStripMenuItem.Checked = bool.Parse(ConfigWrapper.GetSetting("ConversationSettings").FirstOrDefault()["ShowTimeStamps"]);
            }
            else
            {
                timestampToolStripMenuItem.Checked = true;
            }
        }

        private void CreatePluginList()
        {
            foreach (FileInfo tmpDLL in new DirectoryInfo(Path.Combine(Application.StartupPath, "Plugins")).GetFiles("*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    Assembly tmpAssembly = System.Reflection.Assembly.LoadFrom(tmpDLL.FullName);
                    foreach (Type tmpType in tmpAssembly.GetTypes())
                    {
                        try
                        {
                            IConversationPlugin tmpPlugin = (IConversationPlugin)tmpAssembly.CreateInstance(tmpType.FullName);
                            ToolStripMenuItem tmpMenuItem = tmpPlugin.Initialize(this, ThisConnection);

                            //ADD THE MENU ITEM TO THE DROP DOWN LIST
                            if (tmpMenuItem != null)
                            {
                                extraPluginsToolStripMenuItem.DropDownItems.Add(tmpMenuItem);
                                extraPluginsToolStripMenuItem.Visible = true;
                            }

                            //ADD THE PLUGIN TO THE LIST OF LOADED PLUGINS
                            Plugins.Add(tmpPlugin.Name, tmpPlugin);
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
            }
        }

        void EmoticonsDisplay_EmoticonClicked(string EmoticonText)
        {
            //APPEND THE TEXT TO THE SEND BOX AND THEN MOVE THE CARET TO MATCH THE POSITION
            txtSend.Text += " " + EmoticonText;
            txtSend.Select(txtSend.Text.Length, 0);
            txtSend.ScrollToCaret();
        }

        public void EnteredText(string UserName)
        {
            //UPDATE THE STATUS TEXT SO THE USER KNOWS THE OTHER USER HAS ENTERED TEXT
            lblUserEnteredText.Text = string.Format("{0} is typing", UserName);
            tmrEnteredTextClear.Stop();
            tmrEnteredTextClear.Start();
        }

        public void ReceiveMessage(Message_Private IncomingMessage)
        {
            string SelfMessageWithTimeStamp = @"<b style=""color: #0066B3"">{0}</b> ({1}): {2}<br/>";
            string SelfMessageWithoutTimeStamp = @"<b style=""color: #0066B3"">{0}</b>: {2}<br/>";
            string OtherMessageWithTimeStamp = @"<b style=""color: #990099"">{0}</b> ({1}): {2}<br/>";
            string OtherMessageWithoutTimeStamp = @"<b style=""color: #990099"">{0}</b>: {2}<br/>";

            //RUN THROUGH THE LIST OF PLUGINS WHICH SUPPORT RECEIVING OF DATA
            bool DisplayMessage = true;
            foreach (IConversationPlugin tmpPlugin in (from a in Plugins.Values where a.SupportsReceiving == true orderby a.ReceiveOrder select a))
            {
                tmpPlugin.IncomingMessage(ref IncomingMessage, ref DisplayMessage);
            }

            //IF THE MESSAGE IS NOT GOING TO BE DISPLAYED THEN WE DO NOT NEED TO GO ANY FURTHER
            if (!DisplayMessage)
                return;

            //CONVERT KNOWN EMOTICON VALUES TO THEIR IMAGE EQUIV
            foreach (Dictionary<string, string> tmpEmoticon in Emoticons.OrderBy(a => a["text"].Length).Reverse())
            {
                IncomingMessage.Message = Regex.Replace(IncomingMessage.Message, tmpEmoticon["text"], string.Format(@"<img src=""file://{0}/{1}"" height=20 width=20 border=0 style=""vertical-align: middle;"" />", Path.Combine(Application.StartupPath, "Images\\Emoticons\\"), tmpEmoticon["image"]));
            }

            //APPEND THE RECEIVED MESSAGE TO THE DISPLAY WINDOW BY INVOKING THE CONTROL
            if (IncomingMessage.SenderID == ThisConnection.UserID)
                webDisplay.Document.Write(string.Format((timestampToolStripMenuItem.Checked ? SelfMessageWithTimeStamp : SelfMessageWithoutTimeStamp), IncomingMessage.Sender, IncomingMessage.TimeStamp.ToString("M/d/yy h:mm tt"), IncomingMessage.Message));
            else
                webDisplay.Document.Write(string.Format((timestampToolStripMenuItem.Checked ? OtherMessageWithTimeStamp : OtherMessageWithoutTimeStamp), IncomingMessage.Sender, IncomingMessage.TimeStamp.ToString("M/d/yy h:mm tt"), IncomingMessage.Message));

            //TODO: THIS CAN PROBABLY BE MADE TO NOT HAVE TO LOOP THROUGH THE ENTIRE CONVERSATION EVERY TIME
            //INTERCEPT LINK CLICKS BY ADDING AN EVENT HANDLER TO EVERY ONE OF THEM
            for (int i = 0; i < webDisplay.Document.Links.Count; i++)
            {
                webDisplay.Document.Links[i].Click -= ClickHandler;
                webDisplay.Document.Links[i].Click += ClickHandler;

                webDisplay.Document.Links[i].MouseDown -= MouseDownHandler;
                webDisplay.Document.Links[i].MouseDown += MouseDownHandler;
            }

            //ONLY PLAY THE SOUND IF THE SENDER IS NOT YOURSELF
            if (IncomingMessage.Sender != ThisConnection.DisplayName && (!IncomingMessage.IsArchive || FirstMessage))
            {
                Sounds tmpSound = new Sounds();
                tmpSound.Play(Path.Combine(Application.StartupPath, @"Sounds\notify.wav"));
                FirstMessage = false;
            }

            //TODO: LOOK AT HOW SCROLL WORKS WHEN SELECTING TEXT AND STUFF OF THAT NATURE
            //CHECK TO SEE WHERE WE ARE SCROLL WISE AND IF WE NEED TO MOVE THE WINDOW DOWN
            webDisplay.Document.Window.ScrollTo(0, webDisplay.Document.Body.ScrollRectangle.Bottom);

            //IF THE SEND BOX IS NOT FOCUSED THEN START THE WINDOW FLASHING BY INVOKING ACROSS THE THREAD
            if (!txtSend.Focused && !IncomingMessage.IsArchive)
                Common.Windows.FlashWindow.Flash(this, Common.Windows.FLASHWFlags.FLASHW_ALL);

            //CLEAR THE TEXT FROM THE STATUS LABEL
            lblUserEnteredText.Text = "";
        }

        void frmConversation_MouseDown(object sender, HtmlElementEventArgs e)
        {
            if (e.MouseButtonsPressed == MouseButtons.Right)
            {
                //GET THE FILE NAME AND ID FROM THE ELEMENT ON THE FORM
                DownloadingFileName = webDisplay.Document.GetElementFromPoint(e.ClientMousePosition).InnerText;
                DownloadingFileID = Regex.Match(webDisplay.Document.GetElementFromPoint(e.ClientMousePosition).OuterHtml, "getfile://(?<FileID>.*)\"", RegexOptions.IgnoreCase).Groups["FileID"].Value;

                //SHOW THE CONTEXT MENU NOW THAT WE HAVE THE FILE INFORMATION
                contextRightClick.Show(Cursor.Position);
            }
        }

        void webDisplay_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //IF WE ARE NAVIGATING BECAUSE A LINK WAS CLICKED THEN STOP THE NAVIGATION AND OPEN A NEW WINDOW / DOWNLOAD FILE WITH DOWNLOAD FILE MESSAGE
            if (LinkClicked)
            {
                //CANCEL THE NAVIGATION BECAUSE WE ARE GOING TO HANDLE ALL LINKS ON THE PAGE OURSELVES
                e.Cancel = true;

                //IF THE USER CLICKED A LINK OTHER THEN A GETFILE WE WILL JUST OPEN A NEW WINDOW AND PASS THE LINK TO IT
                if (!e.Url.ToString().Contains("getfile://"))
                    webDisplay.Document.Window.Open(e.Url, "_new", "", false);
                else
                {
                    //DISPLAY RECEIVING FILE DIALOG
                    ShowReceiveFileDialog();

                    //CREATE THE GET FILE MESSAGE AND SEND IT TO THE SERVER REQUESTING THE FILE THE USER JUST CLICKED ON
                    new Message_GetFile() { FileID = e.Url.ToString().Replace("getfile://", "").Replace("/", "") }.Send(ThisConnection);
                }
            }

            //STOP THINKING A LINK WAS CLICKED
            LinkClicked = false;
        }

        private void ShowReceiveFileDialog()
        {
            //CREATE A RECEIVE FORM SO THE USER CAN SEE THE APPLICATION IS WORKING
            frmSending tmpFrm = new frmSending();
            tmpFrm.FileName = "Retrieving File";
            tmpFrm.Text = "Get Status";
            tmpFrm.Show();
        }

        private void webDisplay_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //WRITE THE DOCUMENT HEADER TO THE NEW / CLEARED CONVERSATION
            webDisplay.Document.Write(@"<html oncontextmenu=""return false;""><body style=""font-family: Trebuchet MS; font-size: 13px"">");
        }

        void frmConversation_Click(object sender, HtmlElementEventArgs e)
        {
            //REMEMBER THE USER CLICKED A LINK TO NAVIGATE THIS TIME
            LinkClicked = true;
        }

        public void ClearMessages()
        {
            //NAVIGATE TO A BLANK PAGE AND START THE DOCUMENT BODY OFF
            //TODO: GIVE THE USER THE ABILITY TO OVERRIDE THE DEFAULT FONT?
            webDisplay.Navigate("");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //MAKE SURE THE USER IS NOT JUST CLICKING THE SEND BUTTON BUT THEY ACTUALLY HAVE ENTERED SOMETHING
            if (!string.IsNullOrEmpty(txtSend.Text))
            {
                //CREATE A NEW PRIVATE MESSAGE AND SEND IT USING THISCONNECTION
                Message_Private tmpMessage = new Message_Private() { Message = txtSend.Text, ConversationID = this.ConversationID, TimeStamp = DateTime.Now, Sender = ThisConnection.DisplayName, SenderID = ThisConnection.UserID };

                //RUN THE MESSAGE THROUGH ALL OUTBOUND PLUGINS
                bool ContinueToSend = true;
                foreach (IConversationPlugin tmpPlugin in (from a in Plugins.Values where a.SupportsSending == true orderby a.SendOrder select a))
                    tmpPlugin.OutgoingMessage(ref tmpMessage, ref ContinueToSend);

                //SEND THIS MESSAGE
                tmpMessage.Send(ThisConnection);

                //CLEAR THE TEXT SINCE THE MESSAGE WAS SENT
                txtSend.Text = "";
            }

            //HIDE THE EMOTICONS WINDOW AFTER A MESSAGE HAS BEEN SENT
            if (emoticonsToolStripButton.Checked)
            {
                emoticonsToolStripButton.Checked = false;
                EmoticonsDisplay.Visible = false;
            }

            //RESET THE FOCUS ON THE SEND TEXT BOX SO THE USER CAN KEEP TYPING
            txtSend.Focus();
            
            //REMEMBER WE HAVE NOT SENT AN UPDATE FOR THE NEW MESSAGE IF THERE IS ONE
            HasUpdateBeenSent = false;
        }

        private void frmConversation_Activated(object sender, EventArgs e)
        {
            //MAKE SURE THE SEND TEXTBOX ALWAYS HAS THE FOCUS
            txtSend.Focus();
        }

        private void txtReceive_Click(object sender, EventArgs e)
        {
            //MAKE SURE THE SEND TEXTBOX ALWAYS HAS THE FOCUS
            txtSend.Focus();
        }

        private void frmConversation_Shown(object sender, EventArgs e)
        {
            //WHEN THIS FORM IS SHOWN GET THE NEW WINDOW TITLE SINCE IT MAY HAVE CHANGED AFTER THE MESSAGE WAS SENT
            new Message_UpdateMessageTitle() { ConversationID = this.ConversationID }.Send(ThisConnection);
        }

        private void timestampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SWITCH THE CHECK STATE
            timestampToolStripMenuItem.Checked = !timestampToolStripMenuItem.Checked;

            //REQUEST THE CONVERSATION FROM THE SERVER AGAIN SO WE CAN CHANGE THE TIMESTAMP STATE
            ClearMessages();
            new Message_GetConversation() { ConversationID = this.ConversationID }.Send(ThisConnection);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CLOSE THIS WINDOW
            this.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: SAVE THE CONVERSATION FOR LATER USE IF THE USER WANTS TO
        }

        private void sendFileToolStripButton_Click(object sender, EventArgs e)
        {
            //DISPLAY THE OPEN FILE DIALOG AND THEN SEND A FILE TO THE USER
            if (openDialog.ShowDialog() != DialogResult.Cancel)
            {
                string FileName = FileName = new FileInfo(openDialog.FileName).Name;
                new Message_SendFile() { ConversationID = this.ConversationID, FileBytes = File.ReadAllBytes(openDialog.FileName), Sender = ThisConnection.DisplayName, SenderID = ThisConnection.UserID, FileID = Guid.NewGuid().ToString(), FileName = FileName }.Send(ThisConnection);

                //SHOW THE SENDING FORM SO THE USER KNOWS THE APP IS WORKING
                frmSending tmpFrm = new frmSending();
                tmpFrm.FileName = FileName;
                tmpFrm.Show(this);
            }
        }

        private void emoticonsToolStripButton_Click(object sender, EventArgs e)
        {
            //SHOW / HIDE THE EMOTICONS DISPAY WINDOW BASED ON IF IT WERE CHECKED OR NOT
            EmoticonsDisplay.Visible = emoticonsToolStripButton.Checked;

            //FORCE THE EMOTICONS WINDOW TO MOVE EVEN THOUGH THE WINDOW HASNT MOVED
            frmConversation_Move(null, null);
            frmConversation_Resize(null, null);

            //REFOCUS THE CONVERSATION FORM
            this.Focus();
        }

        private void frmConversation_Move(object sender, EventArgs e)
        {
            //MOVE THE EMOTICONS DISPLAY TO THE SAME PLACE
            if (EmoticonsDisplay != null)
                EmoticonsDisplay.Location = new Point(this.Left, this.Bounds.Bottom);
        }

        private void frmConversation_Resize(object sender, EventArgs e)
        {
            //RESIZE THE EMOTICONS DISPLAY TO THE SAME SIZE
            if (Emoticons != null)
                EmoticonsDisplay.Size = new Size(this.Width, EmoticonsDisplay.Height);
        }

        private void frmConversation_FormClosing(object sender, FormClosingEventArgs e)
        {
            //CLOSE THE EMOTICONS WHEN WE CLOSE
            if (EmoticonsDisplay != null)
                EmoticonsDisplay.Close();

            //DISPOSE EACH PLUGIN BECAUSE WE ARE CLOSING
            foreach (IConversationPlugin tmpPlugin in this.Plugins.Values)
                try
                {
                    tmpPlugin.Dispose();
                }
                catch
                {
                }
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SET THE FORM TO TOP MOST OR NOT BASED ON THE CHECK BOX
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }

        private void txtSend_TextChanged(object sender, EventArgs e)
        {
            //SEND THE UPDATE WE HAVE ENTERED TEXT SO EVERYONE CAN SEE WE ARE RESPONDING
            if (!this.HasUpdateBeenSent)
            {
                new Message_EnteredText() { ConversationID = this.ConversationID, Sender = ThisConnection.DisplayName }.Send(ThisConnection);
                HasUpdateBeenSent = true;
                tmrAllowEntered.Start();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SETUP THE DEFAULT OPTIONS SO THE SAVE DIALOG MAKES SENSE TO THE USER
            saveDialog.FileName = DownloadingFileName;
            saveDialog.DefaultExt = DownloadingFileName.Substring(DownloadingFileName.LastIndexOf(".") + 1);

            //DISPLAY THE SAVE DIALOG AND MAKE THE USER CHOOSE WHERE TO SAVE IT THEN SEND THE MESSAGE TO THE SERVER
            if (saveDialog.ShowDialog() != DialogResult.Cancel)
            {
                ShowReceiveFileDialog();
                new Message_GetFile() { FileID = DownloadingFileID, SaveLocation = saveDialog.FileName }.Send(ThisConnection);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //JUST SEND THE MESSAGE TO THE SERVER TO GET THE FILE SO IT WILL OPEN IN IE
            ShowReceiveFileDialog();
            new Message_GetFile() { FileID = DownloadingFileID }.Send(ThisConnection);
        }

        private void tmrAllowEntered_Tick(object sender, EventArgs e)
        {
            //ALLOW ANOTHER UPDATE MESSAGE TO BE SENT THROUGH THE SERVER
            HasUpdateBeenSent = false;
            tmrAllowEntered.Stop();
        }

        private void tmrEnteredTextClear_Tick(object sender, EventArgs e)
        {
            //CLEAR THE TEXT TELLING THE USER THE OTHER USER HAS STARTED TYPING
            lblUserEnteredText.Text = "";
            tmrEnteredTextClear.Stop();
        }
    }
}