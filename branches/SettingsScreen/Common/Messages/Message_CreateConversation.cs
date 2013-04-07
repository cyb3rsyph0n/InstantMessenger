using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using Common.DBAccess;
using System.Windows.Forms;
using Common.Enumerations;

namespace Common.Messages
{
    public class Message_CreateConversation : IServerMessage
    {
        public string ConversationID { get; set; }
        //public string Recipient { get; set; }
        public string[] ConversationMembers {get;set;}

        public override void ServerSide(List<Common.Connections.Connection> ServerConnections, Common.Connections.Connection ThisConnection)
        {
            DBAccess.ConversationDataContext ConversationList = new Common.DBAccess.ConversationDataContext();
            ConversationItem tmpConversation = new ConversationItem();

            //SETUP THE CONVERSTAION FOR THE DATABASE
            tmpConversation.DateCreated = DateTime.Now;
            tmpConversation.ConversationGUID = this.ConversationID;

            //ADD ALL THE MEMBERS TO THE CONVERSATION VIA LINQ
            tmpConversation.ConversationMembers.AddRange((from a in this.ConversationMembers.Distinct() select new ConversationMember { UserID = a }));

            //ADD THE CONVERSATION ITEM TO THE DB CONNECTION AND UPDATE IT
            ConversationList.ConversationItems.InsertOnSubmit(tmpConversation);
            ConversationList.SubmitChanges();

            //SEND THIS MESSAGE BACK TO THE CLIENT SO THEY WILL CREATE A CONVERSATION WINDOW
            Send(ThisConnection);
        }

        public override void ClientSide(System.Windows.Forms.FormCollection OpenWindows, Common.Delegates.CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            Form NewWindow = null;
            CreateWindow("", this.ConversationID, ref NewWindow, WindowType.Conversation);
        }
    }
}
