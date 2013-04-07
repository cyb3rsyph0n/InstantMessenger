using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using System.Windows.Forms;
using Common.Delegates;

namespace Common.Messages
{
    public class Message_KeppAlive : IServerMessage
    {
        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            //JUST REPLY TO THE CLIENT TO KEEP THE CONNECTION ALIVE
            Send(ThisConnection);
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            //DO NOTHING CLIENT SIDE
        }
    }
}
