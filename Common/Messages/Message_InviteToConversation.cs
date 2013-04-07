using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;

namespace Common.Messages
{
    public class Message_InviteToConversation : IServerMessage
    {
        public override void ServerSide(List<Common.Connections.Connection> ServerConnections, Common.Connections.Connection ThisConnection)
        {
            throw new NotImplementedException();
        }

        public override void ClientSide(System.Windows.Forms.FormCollection OpenWindows, Common.Delegates.CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            throw new NotImplementedException();
        }
    }
}
