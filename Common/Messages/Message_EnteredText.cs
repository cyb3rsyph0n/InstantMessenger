using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using System.Windows.Forms;
using Common.Delegates;
using Common.DBAccess;

namespace Common.Messages
{
    public class Message_EnteredText : IServerMessage
    {
        public string ConversationID { get; set; }
        public string Sender { get; set; }

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            //SEND THIS MESSAGE TO EVERYONE ATTACHED TO THE CONVERSATION WINDOW THE USER IS ENTERING TEXT
            using (ConversationDataContext tmpDB = new ConversationDataContext())
            {
                foreach(Connection ServerConnection in (from a in ServerConnections where a!= ThisConnection && (from b in tmpDB.ConversationMembers where b.ConversationItem.ConversationGUID == this.ConversationID select b.UserID).Contains(a.UserID) select a))
                {
                    Send(ServerConnection);
                }
            }
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            //FIND THE WINDOW WHICH HAS THE CORRECT TAG AND UPDATE THE STATUS SO THE USER KNOWS THEY HAVE ENTERED IN TEXT
            foreach (Form OpenWindow in OpenWindows)
            {
                if ((string)OpenWindow.Tag == this.ConversationID)
                {
                    OpenWindow.Invoke((MethodInvoker)delegate { ((IConversationWindow)OpenWindow).EnteredText(this.Sender); });
                }
            }
        }
    }
}
