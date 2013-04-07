using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using System.Windows.Forms;
using Common.Connections;
using Common.Messages;
using System.Drawing;
using Common.Enumerations;
using Common.Config;

namespace WindowPositionSaver
{
    public class PositionSaver : IContactsPlugin, IConversationPlugin
    {
        private Form Window = null;
        private Connection ThisConnection = null;

        public string Name
        {
            get { return "Window Positions"; }
        }

        public Version VersionInfo
        {
            get { return new Version(1, 0); }
        }

        public PluginType PluginType
        {
            get { return PluginType.Contacts | PluginType.Conversation; }
        }

        public Image PropertyPageImage
        {
            get { return Properties.Resources.window_preferences; }
        }

        private Control mPropertyPage = null;
        public Control PropertyPage
        {
            get
            {
                //IF THIS IS THE FIRST TIME THE PROPERTY PAGE IS BEING ASKED FOR THEN CREATE ONE AND HOLD ITS HANDLE
                if (mPropertyPage == null)
                    mPropertyPage = new propertyPage();

                //RETURN THE PROPERTY PAGE SO IT MAY BE DISPLAYED
                return mPropertyPage;
            }
        }

        public bool SupportsSending
        {
            get { return false; }
        }

        public bool SupportsReceiving
        {
            get { return false; }
        }

        public int SendOrder
        {
            get { throw new NotImplementedException(); }
        }

        public int ReceiveOrder
        {
            get { throw new NotImplementedException(); }
        }

        public ToolStripMenuItem Initialize(Form PassedWindow, Connection ThisConnection)
        {
            this.Window = PassedWindow;
            this.ThisConnection = ThisConnection;

            //CHECK TO SEE IF WE ARE EVEN SUPPOSED TO SAVE WINDOW POSITIONS BECAUSE IF WE ARE NOT SAVING THEM THEN WE ARE NOT LOADING THEM EITHER
            if (ConfigWrapper.GetSetting("SaveWindowPositions").FirstOrDefault() == null || bool.Parse(ConfigWrapper.GetSetting("SaveWindowPositions").First()[this.Window.GetType().ToString()]) == false)
                return null;

            //GET THE SETTING FROM THE CONFIG FILE ABOUT WHERE TO PLACE THIS WINDOW
            List<Dictionary<string, string>> tmpReturn = ConfigWrapper.GetSetting("WindowPositions");
            Dictionary<string, string> thisPosition = (from a in tmpReturn where a.Values.Contains((string)this.Window.Tag) select a).FirstOrDefault();

            //IF THERE IS A LOCATION SAVED FOR THIS WINDOW THEN WE NEED TO APPLY IT
            if (thisPosition != null)
            {
                //MAKE SURE THE WINDOW PAYS ATTENTION TO THE LOCATION PREFERENCE WE SET HERE
                this.Window.StartPosition = FormStartPosition.Manual;

                //GET THE LOCATION AND SPLIT THE X AND Y COORIDNATES AND WIDTH AND HEIGHT VALUES
                string[] Location = thisPosition["windowposition"].Split('|');
                string[] Size = thisPosition["windowsize"].Split('|');

                //APPLY THESE VALUES TO THE WINDOW WE ARE CHANGING ITS SETTINGS FOR
                this.Window.Location = new Point(int.Parse(Location[0]), int.Parse(Location[1]));
                this.Window.Size = new Size(int.Parse(Size[0]), int.Parse(Size[1]));
            }

            //THIS PLUGIN DOES NOT HAVE  A MENU OPTION SO RETURN NULL HERE
            return null;
        }

        public void OutgoingMessage(ref Message_Private OutgoingMessage, ref bool ContinueToSend)
        {
            throw new NotImplementedException();
        }

        public void IncomingMessage(ref Message_Private IncomingMessage, ref bool DisplayMessage)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            List<Dictionary<string, string>> tmpReturn = ConfigWrapper.GetSetting("WindowPositions");
            Dictionary<string, string> thisPosition = (from a in tmpReturn where a.Values.Contains((string)this.Window.Tag) select a).FirstOrDefault();

            //IF WE ARE NOT SUPPOSED TO SAVE WINDOW POSITIONS THEN JUST EXIT
            if (ConfigWrapper.GetSetting("SaveWindowPositions").FirstOrDefault() == null || bool.Parse(ConfigWrapper.GetSetting("SaveWindowPositions").First()[this.Window.GetType().ToString()]) == false)
                return;

            //MAKE SURE THE WINDOW IS NOT MINIMIZED BEFORE SAVING ITS SIZE
            if (this.Window.WindowState == FormWindowState.Minimized)
            {
                this.Window.WindowState = FormWindowState.Normal;
                this.Window.Show();
            }

            //IF THIS WINDOW IS NOT SAVED THEN CREATE A NEW DICTIONARY TO SAVE IT ELSE CLEAR THE VALUES SO WE CAN ADD THEM ALL AGAIN
            if (thisPosition == null)
                thisPosition = new Dictionary<string, string>();
            else
                tmpReturn.Remove(thisPosition);

            //CLEAR THE POSITIONS INCASE THIS IS ALREADY SAVED IN THE FILE AND ADD THE VALUES BACK TO IT
            thisPosition.Clear();
            thisPosition.Add("windowtag", (string)this.Window.Tag);
            thisPosition.Add("windowposition", string.Format("{0}|{1}", this.Window.Location.X.ToString(), this.Window.Location.Y.ToString()));
            thisPosition.Add("windowsize", string.Format("{0}|{1}", this.Window.Size.Width.ToString(), this.Window.Size.Height.ToString()));
            tmpReturn.Add(thisPosition);

            //SAVE THIS SETTING TO THE CONFIG FILE
            ConfigWrapper.SaveSetting("WindowPositions", tmpReturn);
        }
    }
}
