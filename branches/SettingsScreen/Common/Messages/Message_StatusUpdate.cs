using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using Common.Delegates;
using System.Windows.Forms;
using System.Drawing;
using Common.DBAccess;

namespace Common.Messages
{
    public class Message_StatusUpdate : IServerMessage
    {
        public string UserID { get; set; }
        public string Status { get; set; }
        public bool Online { get; set; }

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            //STORE THE USERS LAST STATUS IN THE DATABASE
            using (UserStateDataContext tmpDB = new UserStateDataContext())
            {
                //TRY TO SELECT THE USERS LAST KNOWN STATE FROM THE DATABASE
                LastKnownStateItem UserState = (from a in tmpDB.LastKnownStateItems where a.UserID == ThisConnection.UserID select a).FirstOrDefault();

                //IF NOTHING WAS RETURNED WE NEED TO CREATE A NEW ITEM FOR THIS USERS STATE OTHERWISE JUST MODIFY THE EXISTING ONE
                if(UserState == null)
                {
                   UserState = new LastKnownStateItem(){ LastKnownState = this.Status, UserID = ThisConnection.UserID};
                    tmpDB.LastKnownStateItems.InsertOnSubmit(UserState);
                }
                else
                    UserState.LastKnownState = this.Status;

                //STORE THE CHANGES IN THE DATABASE NOW
                tmpDB.SubmitChanges();
            }

            //TODO: THIS COULD BE MORE EFFICIENT IF IT ONLY PASSED THE STATUS TO CONTACTS WHO NEEDED IT
            foreach (Connection tmpConnection in ServerConnections)
                Send(tmpConnection);
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            //FIND THE CONTACTS WINDOW AND EXECUTE THE UPDATE STATUS FUNCTION TO UPDATE THE STATUS OF THE GIVEN CONTACT
            foreach (Form tmpWindow in OpenWindows)
            {
                try
                {
                    //TRY TO CAST THE WINDOW AS A CONTACTWINDOW AND INVOKE THE UPDATE COMMAND
                    IContactsWindow ContactsWindow = (IContactsWindow)tmpWindow;
                    tmpWindow.Invoke((MethodInvoker)delegate { ContactsWindow.UpdateStatus(this); });

                    //BREAK THE LOOP BECAUSE WE FOUND THE WINDOW WE NEED
                    break;
                }
                catch
                {
                }
            }
        }
    }
}
