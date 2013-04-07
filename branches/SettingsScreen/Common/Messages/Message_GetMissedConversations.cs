using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using Common.Delegates;
using System.Windows.Forms;
using Common.DBAccess;
using Common.Config;
using System.IO;
using Common.Enumerations;

namespace Common.Messages
{
    public class Message_GetMissedConversations : IServerMessage
    {
        public Conversation[] MissedConversations { get; set; }
        public bool IsLoginCall { get; set; }

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            IContactsProvider ContactProvider = StaticFunctions.GetContactsProvider();

            using (ConversationDataContext tmpDB = new ConversationDataContext())
            {
                long[] ConvIDS = (from a in tmpDB.MissedConversationItems where a.UserID == ThisConnection.UserID select a.MissedMessageID).ToArray();

                //CREATE THE LIST OF CONVERSATIONS WHICH WE ARE SENDING BACK TO THE USER
                this.MissedConversations = (from a in tmpDB.MissedConversationItems where ConvIDS.Contains(a.MissedMessageID) && a.ConversationItem.MessageItems.Count() > 0 select new Conversation() { ConversationID = a.ConversationItem.ConversationGUID, Date = a.ConversationItem.DateCreated, MessageCount = a.ConversationItem.MessageItems.Count(), Recipients = string.Join("; ", a.ConversationItem.ConversationMembers.Select(b => ContactProvider.UserNameFromID(b.UserID)).ToArray()) }).ToArray();

                //IF THERE ARE MISSED CONVERSATIONS THEN REPLY TO THE USER
                Send(ThisConnection);
            }
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            Form FoundWindow = null;
            IRecentConversationsWindow ConvWindow = null;

            //IF THERE WERE MISSED CONVERSATIONS THEN SHOW THE MISSED CONVERSATIONS WINDOW
            //TODO: NEED TO SHOW THE WINDOW ANYWAY AFTER THE INITIAL LOGIN HAS CHECKED FOR MISSED CONVERSATIONS
            if (this.MissedConversations.Count() != 0 || !IsLoginCall)
            {

                foreach (Form tmpWindow in OpenWindows)
                {
                    try
                    {
                        ConvWindow = (IRecentConversationsWindow)tmpWindow;
                        break;
                    }
                    catch
                    {
                    }
                }

                if (ConvWindow == null)
                    CreateWindow(null, null, ref FoundWindow, WindowType.MissedConversationList);

                FoundWindow.Invoke((MethodInvoker)delegate
                {
                    ConvWindow = (IRecentConversationsWindow)FoundWindow;
                    ConvWindow.RecentList = this.MissedConversations;
                });
            }
        }

        public class Conversation
        {
            public DateTime Date { get; set; }
            public string Recipients { get; set; }
            public int MessageCount { get; set; }
            public string ConversationID { get; set; }
        }
    }
}
