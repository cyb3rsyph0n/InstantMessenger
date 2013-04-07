using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Messages;
using Common.Interfaces;
using Common.Connections;
using Common.Delegates;
using System.Windows.Forms;
using Common.DBAccess;
using Common.Enumerations;

namespace Common.Messages
{
    public class Message_Private : IServerMessage
    {
        public string ConversationID { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public string SenderID { get; set; }
        public bool IsArchive { get; set; }
        public DateTime TimeStamp { get; set; }

        public Message_Private()
        {
            this.IsArchive = false;
        }

        #region IServerMessage Members

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            //GET THE CONVERSATION FROM THE DATABASE AND SELECT WHICH USERS ARE ATTACHED TO IT
            string[] UserIDs = (from a in new DBAccess.ConversationDataContext().ConversationMembers where a.ConversationItem.ConversationGUID == this.ConversationID select a.UserID).ToArray();

            //REPLACE THE TIME STAMP WITH SERVER TIME
            this.TimeStamp = DateTime.Now;

            //THIS SENDS THE CONVERSATION TO THE PERSON WHO STARTED IT
            Send(ThisConnection);

            //FIND THE UNIQUE ID OF THE PERSON THIS MESSAGE IS TO
            foreach (Connection tmpConnection in ServerConnections)
                if (UserIDs.Contains(tmpConnection.UserID) && tmpConnection != ThisConnection)
                    Send(tmpConnection);

            //STORES THIS MESSAGE IN THE DATABASE FOR LATER RECALL
            using (ConversationDataContext tmpDB = new DBAccess.ConversationDataContext())
            {
                MessageItem tmpMessage = new MessageItem() { MessageData = MessageWrapper.PackageForTCP(this.GetType(), this) };
                tmpDB.ConversationItems.Single(a => a.ConversationGUID == this.ConversationID).MessageItems.Add(tmpMessage);

                //FLAG ANY USER WHICH HAS MISSED THIS CONVERSATION BECAUSE THEY ARE OFFLINE
                string[] ConnectedIDs = (from a in ServerConnections select a.UserID).ToArray();
                string[] OfflineIDS = (from a in UserIDs where ConnectedIDs.Contains(a) == false select a).ToArray();

                //NOW ONLY SELECT USERID'S WHICH ARE NOT ALREADY FLAGGED AS HAVING MISSED THIS CONVERSATION
                OfflineIDS = (from a in OfflineIDS where tmpMessage.ConversationItem.MissedConversationItems.Select(b => b.UserID).Contains(a) == false select a).ToArray();

                //ADD THE USERS TO THE TABLE WITH THE MESSAGE ID SO THEY CAN SEE WHICH CONVERSATIONS THEY HAVE MISSED
                tmpMessage.ConversationItem.MissedConversationItems.AddRange(from a in OfflineIDS select new MissedConversationItem { UserID = a });

                //UPDATE THE DATABASE
                tmpDB.SubmitChanges();
            }
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            bool createdForm = false;
            Form foundForm = null;

            //LOOP THROUGH AND LOOK FOR A WINDOW WITH THE CORRECT MESSAGE ID
            foreach (Form tmpForm in OpenWindows)
            {
                //IF WE FIND ONE REMEMBER WHICH ONE IT IS AND MOVE ON
                if ((string)tmpForm.Tag == this.ConversationID)
                {
                    foundForm = tmpForm;
                    break;
                }
            }

            //IF WE DO NOT FIND ONE THEN WE NEED TO CREATE ONE
            if (foundForm == null)
            {
                createdForm = true;
                CreateWindow(this.Sender, this.ConversationID, ref foundForm, WindowType.Conversation);
            }

            //SHOW THE FORM IF THIS IS THE FIRST TIME IT WAS CREATED
            if (createdForm)
            {
                foundForm.Invoke((MethodInvoker)delegate
                {
                    foundForm.Show();
                    ((IConversationWindow)foundForm).ClearMessages();
                });
                new Message_GetConversation() { ConversationID = this.ConversationID }.Send(ThisConnection);
                return;
            }

            //USE THE INTERFACE TO PASS THE MESSAGE TO THE WINDOW WHICH WE FOUND
            IConversationWindow tmpConversation = (IConversationWindow)foundForm;
            foundForm.Invoke((MethodInvoker)delegate { tmpConversation.ReceiveMessage(this); });
        }

        #endregion
    }
}
