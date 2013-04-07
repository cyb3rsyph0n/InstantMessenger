using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using System.Windows.Forms;
using Common.Delegates;
using Common.DBAccess;
using Common.Config;
using System.IO;
using Common.Enumerations;

namespace Common.Messages
{
    public class Message_GetRecentConversations : IServerMessage
    {
        public int DaysToSearch { get; set; }
        public Conversation[] RecentList { get; set; }

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            IContactsProvider ContactProvider = StaticFunctions.GetContactsProvider();

            using (ConversationDataContext tmpDB = new ConversationDataContext())
            {
                long[] ConvIDS = (from a in tmpDB.ConversationMembers where a.UserID == ThisConnection.UserID select a.ConversationID).ToArray();
                ConversationItem[] ConvList = (from a in tmpDB.ConversationItems where ConvIDS.Contains(a.ConversationID) select a).ToArray();

                //GET THE RECENT MESSAGES FROM THE DATABASE
                this.RecentList = (from a in ConvList where a.MessageItems.Count() > 0 && a.DateCreated > DateTime.Now.AddDays(this.DaysToSearch * -1) select new Conversation() { Date = a.DateCreated, Preview = ((Message_Private)MessageWrapper.UnPackageFromTCP(a.MessageItems.First().MessageData.ToArray(), true)).Message, Recipients = string.Join("; ", a.ConversationMembers.Select(b=>ContactProvider.UserNameFromID(b.UserID)).OrderBy(c=>c).ToArray()), MessageCount = a.MessageItems.Count(), ConversationID = a.ConversationGUID }).OrderBy(a=>a.Date).Reverse().ToArray();

                //SEND THE INFORMATION BACK TO THE USER
                Send(ThisConnection);
            }
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Common.Connections.Connection ThisConnection)
        {
            Form FoundWindow = null;
            IRecentConversationsWindow ConvWindow = null;

            //LOOP THROUGH EACH OPEN WINDOW LOOKING FOR THE CONV WINDOW
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

            //IF WE DIDNT FIND THE CONVERSATION LIST WINDOW THEN CREATE A NEW ONE
            if (ConvWindow == null)
                CreateWindow(null, null, ref FoundWindow, WindowType.RecentConversationList);

            //INVOKE THE CHANGING OF THE DATASOURCE ON THE WINDOW
            FoundWindow.Invoke((MethodInvoker)delegate
            {
                ConvWindow = (IRecentConversationsWindow)FoundWindow;
                ConvWindow.RecentList = this.RecentList;
            });
        }

        public class Conversation
        {
            public DateTime Date { get; set; }
            public string Recipients { get; set; }
            public string Preview { get; set; }
            public int MessageCount { get; set; }
            public string ConversationID { get; set; }
        }
    }
}
