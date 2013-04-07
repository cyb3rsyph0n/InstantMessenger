using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Interfaces;
using Common.Config;

namespace WindowPositionSaver
{
    public partial class propertyPage : UserControl, IPropertyPage
    {
        public propertyPage()
        {
            InitializeComponent();

            //LOAD THE INITIAL SETTINGS FROM THE CONFIG FILE
            LoadSettings();
        }

        public void SaveSettings()
        {
            Dictionary<string, string> tmpValues = new Dictionary<string, string>();
            tmpValues.Add("Client.frmContacts", chkContactsWindow.Checked.ToString());
            tmpValues.Add("Client.frmConversation", chkConversationWindows.Checked.ToString());
            ConfigWrapper.SaveSetting("SaveWindowPositions", new List<Dictionary<string, string>>() { tmpValues });
        }

        public void LoadSettings()
        {
            if (ConfigWrapper.GetSetting("SaveWindowPositions").FirstOrDefault() != null)
            {
                chkContactsWindow.Checked = bool.Parse(ConfigWrapper.GetSetting("SaveWindowPositions").FirstOrDefault()["Client.frmContacts"]);
                chkConversationWindows.Checked = bool.Parse(ConfigWrapper.GetSetting("SaveWindowPositions").FirstOrDefault()["Client.frmConversation"]);
            }
        }

        public bool HavePropertiesChanged
        {
            get { throw new NotImplementedException(); }
        }
    }
}