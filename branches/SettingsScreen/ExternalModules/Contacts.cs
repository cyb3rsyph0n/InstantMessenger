using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Delegates;
using Common.Messages;

namespace ExternalModules
{
    public class Contacts : IContactsProvider
    {
        #region IContactsProvider Members

        public Message_GetContacts.Contact[] GetContacts(string UserID)
        {
            List<Message_GetContacts.Contact> tmpReturn = new List<Message_GetContacts.Contact>();
            tmpReturn.Add(new Message_GetContacts.Contact() { DName = UserID, UserID = UserID, Group = "Friends" });

            //using (DBAccess.MCSAuthDataContext tmpDB = new ExternalModules.DBAccess.MCSAuthDataContext())
            //{
            //    tmpReturn.AddRange((from a in tmpDB.UserItems where a.is_active == true && a.user_type_id == 1 && a.cust_id == 17 select new Message_GetContacts.Contact { DName = a.user_fullname, UserID = a.user_id, Group = "MCS" }).ToArray());
            //    tmpReturn.AddRange((from a in tmpDB.UserItems where a.is_active == true && a.vendor_id != 0 && a.VendorItem.is_active == true select new Message_GetContacts.Contact { DName = a.user_fullname, UserID = a.user_id, Group = "Vendors\\" + a.VendorItem.name }).ToArray());

            //    return tmpReturn.ToArray();
            //}
            return tmpReturn.ToArray();
        }

        public string UserNameFromID(string UserID)
        {
            return UserID;
            //using (DBAccess.MCSAuthDataContext tmpDB = new ExternalModules.DBAccess.MCSAuthDataContext())
            //{
            //    return (from a in tmpDB.UserItems where a.user_id == UserID select a.user_fullname).FirstOrDefault();
            //}
        }

        #endregion
    }
}
