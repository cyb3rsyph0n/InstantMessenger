using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Messages;
using Common.Interfaces;
using Common.Connections;
using Common.Delegates;
using System.Windows.Forms;

namespace Common.Messages
{
    public class Message_Global : IServerMessage
    {
        public string ConversationID { get; set; }
        public string Message { get; set; }

        #region IServerMessage Members

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            foreach (Connection ServerConnection in ServerConnections)
            {
                Send(ServerConnection);
            }
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            System.Windows.Forms.MessageBox.Show(this.Message);
        }

        #endregion
    }
}
