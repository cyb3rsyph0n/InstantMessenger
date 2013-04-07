using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Connections;
using System.Windows.Forms;
using Common.Interfaces;
using Common.Delegates;

namespace Common.Messages
{
    public class Message_BounceBack : IServerMessage
    {
        public string Message { get; set; }

        #region IServerMessage Members

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            Send(ThisConnection);
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            System.Windows.Forms.MessageBox.Show("Bounce Back Received: " + this.Message);
        }

        #endregion
    }
}
