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

namespace Client.SettingsControls
{
    public partial class propertyPageConversations : UserControl, IPropertyPage
    {
        public propertyPageConversations()
        {
            InitializeComponent();
            LoadSettings();
        }

        #region IPropertyPage Members

        public bool HavePropertiesChanged
        {
            get { throw new NotImplementedException(); }
        }

        public void SaveSettings()
        {
            Dictionary<string, string> tmpSettings = new Dictionary<string, string>();
            tmpSettings.Add("ShowTimeStamps", chkTimeStamps.Checked.ToString());
            tmpSettings.Add("CheckMissedConversations", chkMissedConversations.Checked.ToString());
            tmpSettings.Add("ConversationHistory", numConversationDays.Value.ToString());

            ConfigWrapper.SaveSetting("ConversationSettings", new List<Dictionary<string, string>>() { tmpSettings });
        }

        public void LoadSettings()
        {
            if (ConfigWrapper.GetSetting("ConversationSettings").FirstOrDefault() != null)
            {
                chkTimeStamps.Checked = bool.Parse(ConfigWrapper.GetSetting("ConversationSettings").FirstOrDefault()["ShowTimeStamps"]);
                chkMissedConversations.Checked = bool.Parse(ConfigWrapper.GetSetting("ConversationSettings").FirstOrDefault()["CheckMissedConversations"]);
                numConversationDays.Value = int.Parse(ConfigWrapper.GetSetting("ConversationSettings").FirstOrDefault()["ConversationHistory"]);
            }
        }

        #endregion
    }
}
