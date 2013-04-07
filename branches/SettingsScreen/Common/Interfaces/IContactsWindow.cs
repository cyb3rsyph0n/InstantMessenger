using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Messages;

namespace Common.Interfaces
{
    public interface IContactsWindow
    {
        //RadTreeView ContactsList { get; }
        void DisplayContacts(Message_GetContacts.Contact[] ContactList);
        void UpdateStatus(Message_StatusUpdate newStatus);
    }
}
