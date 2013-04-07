using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Config;
using System.IO;
using System.Windows.Forms;

namespace Common.Messages
{
    public static class StaticFunctions
    {
        public static string AppDataPath
        {
            get
            {
                //RETURN THE DEFAULT APPLICATION DATA PATH
                return Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Instant Messenger\\" + Application.ProductName);
            }
        }

        public static Type GetObjType(string TypeName)
        {
            return Type.GetType(TypeName);
        }

        public static IContactsProvider GetContactsProvider()
        {
            Dictionary<string, string> ContactSetting = ConfigWrapper.GetSetting("ContactsProvider")[0];
            return (IContactsProvider)Activator.CreateInstanceFrom(Path.Combine(Application.StartupPath, @"ExternalModules\" + ContactSetting["file"]), ContactSetting["library"]).Unwrap();
        }

        public static IAuthenticationProvider GetAuthenticationProvider()
        {
            Dictionary<string, string> AuthSetting = ConfigWrapper.GetSetting("AuthenticationProvider")[0];
            return (IAuthenticationProvider)Activator.CreateInstanceFrom(Path.Combine(Application.StartupPath, @"ExternalModules\" + AuthSetting["file"]), AuthSetting["library"]).Unwrap();
        }
    }
}
