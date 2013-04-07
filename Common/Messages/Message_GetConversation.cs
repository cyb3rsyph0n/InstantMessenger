using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.DBAccess;
using System.Windows.Forms;

namespace Common.Messages
{
    public class Message_GetConversation : IServerMessage
    {
        public string ConversationID { get; set; }

        public override void ServerSide(List<Common.Connections.Connection> ServerConnections, Common.Connections.Connection ThisConnection)
        {
            Send(ThisConnection);
            System.Threading.Thread.Sleep(50);

            using (ConversationDataContext tmpDB = new ConversationDataContext())
            {
                List<MessageItem> Messages = ((ConversationItem)(from a in tmpDB.ConversationItems where a.ConversationGUID == this.ConversationID select a).Single()).MessageItems.OrderBy(a => a.MessageID).ToList();

                //LOOP THROUGH EACH MESSAGE IN THE DATABASE AND SEND IT BACK TO THE PERSON REQUESTING THE CONVERSATION
                foreach (MessageItem tmpMessage in Messages)
                {
                    //UNWRAP THE MESSAGE AND SEND IT BACK TO THE SAME CONNECTION WHICH ASKED FOR IT
                    Message_Private tmpPrivateMessage = (Message_Private)MessageWrapper.UnPackageFromTCP(tmpMessage.MessageData.ToArray(), true);
                    
                    //USED TO STOP THE SOUND FROM PLAYING SINCE THIS MESSAGE IS AN ARCHIVED MESSAGE WE DO NOT NEED TO NOTIFY THE USER IT HAS ARIVED
                    tmpPrivateMessage.IsArchive = true;
                    
                    //SEND THE TMP MESSAGE BACK
                    tmpPrivateMessage.Send(ThisConnection);

                    //SLEEP BRIEFLY TO SLOW MESSAGE ARIVAL ON THE CLIENT
                    System.Threading.Thread.Sleep(50);
                }

                //CHECK TO SEE IF WE NEED TO DELETE THE ITEM FROM A MISSED CONVERSATION LIST OR NOT
                tmpDB.MissedConversationItems.DeleteAllOnSubmit(from a in tmpDB.MissedConversationItems where a.ConversationItem.ConversationGUID == this.ConversationID select a);
                tmpDB.SubmitChanges();
            }
        }

        public override void ClientSide(System.Windows.Forms.FormCollection OpenWindows, Common.Delegates.CreateClientWindow CreateWindow, Common.Connections.Connection ThisConnection)
        {
            //THIS DOESNT NEED TO DO ANYTHING CLIENT SIDE BECAUSE IT EXECUTES A SERIES OF PRIVATE MESSAGES FOR EACH MESSAGE FOUND IN THE DATABASE
        }
    }
}
