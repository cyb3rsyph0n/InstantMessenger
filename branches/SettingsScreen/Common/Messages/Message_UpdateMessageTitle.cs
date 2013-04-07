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

namespace Common.Messages
{
    public class Message_UpdateMessageTitle : IServerMessage
    {
        public string ConversationID { get; set; }
        public string[] Recipients { get; set; }

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            IContactsProvider ContactProvider = StaticFunctions.GetContactsProvider();

            //RETURN THE LIST OF USERS WHO ARE ATTACHED TO THIS CONVERSATION SO THE TITLE OF A WINDOW MAY BE UPDATED
            using (ConversationDataContext tmpDB = new ConversationDataContext())
            {
                this.Recipients = (from a in tmpDB.ConversationMembers where a.ConversationItem.ConversationGUID == this.ConversationID select ContactProvider.UserNameFromID(a.UserID)).ToArray();
            }

            //SEND THE MESSAGE TO THE CLIENT
            Send(ThisConnection);
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow,Connection ThisConnection)
        {
            //FIND THE WINDOW ASSOCIATED TO THIS CONVERSATION ID AND UPDATES ITS TITLE
            foreach (Form OpenWindow in OpenWindows)
            {
                if ((string)OpenWindow.Tag == this.ConversationID)
                {
                    OpenWindow.Invoke((MethodInvoker)delegate { OpenWindow.Text = string.Format("{0} - Conversation", string.Join("; ", this.Recipients)); });
                }
            }
        }
    }
}
