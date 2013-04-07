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
    public class Message_HandShake : IServerMessage
    {
        #region IServerMessage Members

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            Send(ThisConnection);
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            //DO NOTHING THIS IS JUST TO GET DATA FLOWING
        }

        public override void Send(Connection ThisConnection)
        {
            base.Send(ThisConnection, true);
        }

        #endregion
    }
}
