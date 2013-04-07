using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common.Messages;
using Common.Connections;
using Common.Enumerations;
using System.Drawing;

namespace LinkConverter
{
    public class Converter : IConversationPlugin
    {
        private ToolStripMenuItem MainMenu;
        private Connection ThisConnection;
        private IConversationWindow Window;

        public bool SupportsSending
        {
            get { return false; }
        }

        public bool SupportsReceiving
        {
            get { return true; }
        }

        public int SendOrder
        {
            get { throw new NotImplementedException(); }
        }

        public int ReceiveOrder
        {
            get { return 0; }
        }

        public Image PropertyPageImage
        {
            get
            {
                return Properties.Resources.link;
            }
        }

        public ToolStripMenuItem Initialize(Form ConversationWindow, Connection ThisConnection)
        {
            this.MainMenu = new ToolStripMenuItem(string.Format("{0} v{1}", this.Name, this.VersionInfo.ToString()));
            this.MainMenu.CheckOnClick = true;
            this.MainMenu.Checked = true;
            this.MainMenu.CheckedChanged += new EventHandler(MainMenu_CheckedChanged);
            this.ThisConnection = ThisConnection;
            this.Window = (IConversationWindow)ConversationWindow;

            return this.MainMenu;
        }

        void MainMenu_CheckedChanged(object sender, EventArgs e)
        {
            //REGET THE MESSAGE WITH THE NEW CHECKED STATE ENABLED
            this.Window.ClearMessages();
            new Message_GetConversation() { ConversationID = this.Window.ConversationID }.Send(this.ThisConnection);
        }

        public void OutgoingMessage(ref Message_Private OutgoingMessage, ref bool ContinueToSend)
        {
            throw new NotImplementedException();
        }

        public void IncomingMessage(ref Message_Private IncomingMessage, ref bool DisplayMessage)
        {
            //WE NEED TO SCAN THE INCOMING MESSAGE VIA REGEX FOR URL'S WE NEED TO CONVERT TO <A></A>
            //TODO: EMAIL LINKS ACTUALLY CAUSE AN ERROR SO WE HANDLE BY BREAKING THE LINK SO IT WONT WORK
            string hostregex = @"([a-z\d][-a-z\d]+[a-z\d]\.)+[a-z][-a-z\d]+[a-z]";
            string portregex = @"(:\d{1,})?";
            string pathregex = @"(/[^\s]+)?";
            string queryregex = "(\\?[^<>#\"\\string]+)?";
            string fullregex = @"(?:(?<=^)|(?<=\s))(((ht|f)tps?://)?" + hostregex + portregex + pathregex + queryregex + ")";

            //IF THE MENU ITEM IS NOT CHECKED THEN WE DONT NEED TO RUN THIS ITEM
            if (!this.MainMenu.Checked)
                return;

            //REPLACE URL STRINGS WITH ACTUAL LINKS SO THEY CAN BE NAVIGATED TO
            Regex tmpScan = new Regex(fullregex, RegexOptions.IgnoreCase);
            foreach (Match tmpMatch in tmpScan.Matches(IncomingMessage.Message))
            {
                string newValue = (tmpMatch.Value.Contains("://") ? tmpMatch.Value : "http://" + tmpMatch.Value);
                IncomingMessage.Message = IncomingMessage.Message.Replace(tmpMatch.Value, string.Format("<a href=\"{0}\" target=\"_new\">{1}</a>", newValue, tmpMatch.Value));
            }
        }

        public string Name
        {
            get { return "URL Link Converter"; }
        }

        public Version VersionInfo
        {
            get { return new Version(1,0); }
        }

        public PluginType PluginType
        {
            get { return PluginType.Conversation; }
        }

        private Control mPropertyPage = null;

        public Control PropertyPage
        {
            get
            {
                if (mPropertyPage == null)
                    mPropertyPage = new propertyPage();
                return mPropertyPage;
            }
        }

        public void Dispose()
        {
        }
    }
}
