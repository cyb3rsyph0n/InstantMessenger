using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Messages;
using Common.Delegates;

namespace Common.Interfaces
{
    public interface IContactsProvider
    {
        Message_GetContacts.Contact[] GetContacts(string UserID);
        string UserNameFromID(string UserID);
    }
}