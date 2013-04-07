using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Deployment.Application;
using Microsoft.Win32;

namespace Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                RegistryKey tmpKey = Registry.CurrentUser.CreateSubKey("Software\\MCS\\InstantMessenger");
                tmpKey.SetValue("InstallPath", Application.ExecutablePath);
            }

            while (!Globals.Exiting)
            {
                frmLogin tmpLogin = new frmLogin();
                Application.Run(tmpLogin);

                string userName = tmpLogin.UserName;
                string passWord = tmpLogin.Password;
                tmpLogin.Close();
                tmpLogin.Dispose();

                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(passWord))
                {
                    frmContacts tmpContacts = new frmContacts(userName, passWord);
                    if(tmpContacts.Visible)
                        Application.Run(tmpContacts);
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
