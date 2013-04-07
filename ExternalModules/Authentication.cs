using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Delegates;
using Common.Structures;
using System.Security.Cryptography;

namespace ExternalModules
{
    public class Authentication : IAuthenticationProvider
    {
        #region IAuthenticationProvider Members

        private string HashPassword(string password)
        {
            return BitConverter.ToString(MD5.Create().ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(password))).Replace("-", "");
        }
        public AuthResult Authenticate(string UserName, string Password)
        {
            try
            {
                using (DBAccess.MCSAuthDataContext tmpDB = new ExternalModules.DBAccess.MCSAuthDataContext())
                {
                    AuthResult tmpResult = (from a in tmpDB.UserItems where a.user_name == UserName && (a.user_pwd == Password || a.user_pwd == HashPassword(Password)) && a.user_type_id != 2 orderby a.user_type_id select new AuthResult { UserID = a.user_id, Success = true, DisplayName = a.user_fullname }).FirstOrDefault();
                    return tmpResult;
                }
            }
            catch
            {
                return new AuthResult() { Success = false };
            }
        }

        #endregion
    }
}
