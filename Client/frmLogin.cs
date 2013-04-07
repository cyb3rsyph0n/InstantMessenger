using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Config;
using System.IO;
using Telerik.WinControls.UI;
using System.Deployment.Application;

namespace Client
{
    public partial class frmLogin : Form
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //CHECK TO MAKE SURE THERE IS NOT A NEWER VERSION AVAILABLE AND DOWNLOAD IF THERE IS
            if (ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.CheckForUpdate() == true)
            {
                MessageBox.Show("A newer version of this software exists and will be downloaded and installed now.");
                ApplicationDeployment.CurrentDeployment.Update();
                MessageBox.Show("This application will now exit and restart");
                Globals.Exiting = true;
                Application.Restart();
                return;
            }
            
            //IF THE SAVE PASSWORD BUTTON IS CHECKED THEN WE WILL NEED TO ADD IT TO THE SETTINGS FILE
            if (chkSavePassword.Checked)
            {
                List<Dictionary<string, string>> tmpUserNames = new List<Dictionary<string, string>>((from a in ConfigWrapper.GetSetting("userList") where !a.Values.Contains(txtUserName.Text) select a).ToArray());
                Dictionary<string, string> tmpDict = new Dictionary<string, string>();

                //ADD THE CURRENT USER AND THEIR PASSWORD TO THE SAVED LIST
                tmpDict.Add("name", txtUserName.Text);
                tmpDict.Add("password", ConfigWrapper.EncryptString(txtPassword.Text));
                tmpUserNames.Add(tmpDict);

                //SAVE THE UPDATED LIST TO THE CONFIG FILE
                ConfigWrapper.SaveSetting("userList", tmpUserNames);
            }

            //SAVE THE LAST USER WHICH WAS SELECTED FROM THE DROP DOWN BOX FOR NEXT TIME
            Dictionary<string, string> tmpSetting = new Dictionary<string, string>();
            tmpSetting.Add("name", txtUserName.Text);
            ConfigWrapper.SaveSetting("lastuser", new List<Dictionary<string, string>>() { tmpSetting });

            //CLOSE THIS FORM SINCE THE USER IS DONE WITH IT
            if (!string.IsNullOrEmpty(txtUserName.Text) || !string.IsNullOrEmpty(txtPassword.Text))
            {
                this.UserName = txtUserName.Text;
                this.Password = txtPassword.Text;
                this.Close();

                //MAKE NOTE THE USER IS NOT MEANING TO EXIT JUST LOGIN
                Globals.Exiting = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //EXIT THE WHOLE APPLICATION
            Globals.Exiting = true;
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //POPULATE THE LIST WITH ALL THE USERS WHO HAVE LOGGED IN THROUGH THIS COMPUTER
            foreach (Dictionary<string, string> tmpName in ConfigWrapper.GetSetting("userList"))
                txtUserName.Items.Add(new RadComboBoxItem() { Text = tmpName.Values.First() });

            //SELECT THE LAST USERNAME FROM THE CONFIG FILE AND DISPLAY IT IN THE LOGIN SCREEN
            txtUserName.SelectedItem = txtUserName.Items.Where(a => a.Text == ConfigWrapper.GetSetting("lastuser")[0]["name"]).FirstOrDefault();
        }

        private void txtUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, string> tmpPassword = (from a in ConfigWrapper.GetSetting("userList") where a.Values.Contains(txtUserName.Text) select a).FirstOrDefault();

            //CHECK TO SEE IF THE SELECTED USER HAS A PASSWORD IN THE CONFIG FILE AND IF THEY DO THEN POPULATE IT
            if (tmpPassword != null && !string.IsNullOrEmpty(tmpPassword["password"]))
            {
                txtPassword.Text = ConfigWrapper.DecryptString(tmpPassword["password"]);
                chkSavePassword.Checked = true;
            }
            else
            {
                //OTHERWISE REMOVE THE TEXT FROM THE PASSWORD AND UNCHECK THE SAVE PASSWORD BOX
                txtPassword.Text = "";
                chkSavePassword.Checked = false;
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //IF THE USER CLOSED THE SCREEN THEN WE ARE EXITING THE APPLICATION OTHER WISE IT MUST HAVE BEEN A LOGIN CLICK
            if (e.CloseReason == CloseReason.UserClosing)
                Globals.Exiting = true;
        }
    }
}
