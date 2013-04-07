using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using Common.Delegates;
using System.Windows.Forms;
using Common.Config;
using System.IO;
using System.Drawing;
using Common.DBAccess;

namespace Common.Messages
{
    public class Message_GetContacts : IServerMessage
    {
        public Contact[] ContactList;

        #region IServerMessage Members

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            IContactsProvider ContactProvider = StaticFunctions.GetContactsProvider();

            //USE THE EXTERNAL LIBRARY TO GET THE CONTACTS FROM WHEREVER CONTACTS ARE PULLED
            this.ContactList = ContactProvider.GetContacts(ThisConnection.UserID).OrderBy(a=>a.DName).OrderBy(a=>a.Group).ToArray();           

            //SET THE USER TO ONLINE IF THEY ARE FOUND IN OUR CONNECTED LIST
            foreach (Contact tmpContact in this.ContactList)
                if ((from a in ServerConnections where a.UserID == tmpContact.UserID select a).Count() != 0)
                    tmpContact.Online = true;

            //GET THE USERS LAST KNOWN STATE FROM OUR DB
            using (UserStateDataContext tmpDB = new UserStateDataContext())
            {
                //LOOP THROUGH ALL OFFLINE CONTACTS AND MARK THEM AS OFFLINE
                foreach (Contact tmpContact in (from a in this.ContactList where a.Online == false select a))
                    tmpContact.LastStatus = "Offline";

                //LOOP THROUGH ALL ONLINE CONTACTS AND TAKE THEIR STATUS FROM THE DATABASE
                foreach (Contact tmpContact in (from a in this.ContactList where a.Online == true select a))
                {
                    tmpContact.LastStatus = (from b in tmpDB.LastKnownStateItems where b.UserID == tmpContact.UserID select b.LastKnownState).FirstOrDefault();
                }
            }

            //SEND THIS BACK TO THE CLIENT
            Send(ThisConnection);
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            //SLEEP FOR 1 SECOND WHILE THE CONTACTS WINDOW LOADS JUST INCASE
            System.Threading.Thread.Sleep(1000);

            //LOOP THROUGH ALL OPEN WINDOWS UNTIL WE FIND THE CONTACTS WINDOW
            foreach (Form tmpWindow in OpenWindows)
            {
                try
                {
                    //CAST THE WINDOW AS A CONTACTS WINDOW AND EXECUTE THE DISPLAYCONTACTS FUNCTION
                    IContactsWindow ContactsWindow = (IContactsWindow)tmpWindow;
                    tmpWindow.Invoke((MethodInvoker)delegate { ContactsWindow.DisplayContacts(this.ContactList); });

                    break;
                }
                catch
                {

                }
            }
        }

        #endregion

        public class Contact
        {
            public string FName { get; set; }
            public string LName { get; set; }
            public string DName { get; set; }
            public string UserID { get; set; }
            public string Group { get; set; }
            public DateTime LastLogin { get; set; }
            public bool Online {get;set;}
            public string LastStatus { get; set; }
            public string[] ExtraInfo{get;set;}
        }
    }
}