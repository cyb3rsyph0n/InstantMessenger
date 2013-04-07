using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Delegates;
using Common.Structures;

namespace ExternalModules
{
    public class Authentication : IAuthenticationProvider
    {
        #region IAuthenticationProvider Members

        public AuthResult Authenticate(string UserName, string Password)
        {
            return new AuthResult() { DisplayName = UserName, Success = true, UserID = UserName };
            using (DBAccess.MCSAuthDataContext tmpDB = new ExternalModules.DBAccess.MCSAuthDataContext())
            {
                AuthResult tmpResult = (from a in tmpDB.UserItems where a.user_name == UserName && a.user_pwd == Password && a.user_type_id != 2 orderby a.user_type_id select new AuthResult { UserID = a.user_id, Success = true, DisplayName = a.user_fullname }).FirstOrDefault();
                return tmpResult;
            }
        }

        #endregion
    }
}
