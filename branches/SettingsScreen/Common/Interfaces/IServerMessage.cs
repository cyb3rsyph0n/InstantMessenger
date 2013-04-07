using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Connections;
using Common.Delegates;
using Common.Messages;

namespace Common.Interfaces
{
    public abstract class IServerMessage
    {
        public abstract void ServerSide(List<Connection> ServerConnections, Connection ThisConnection);
        public abstract void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection);

        public virtual void Send(Connection ThisConnection)
        {
            Send(ThisConnection, false);
        }

        public void Send(Connection ThisConnection, bool OverrideAuth)
        {
            if (ThisConnection.Authenticated || OverrideAuth)
            {
                byte[] outBuffer = MessageWrapper.PackageForTCP(this.GetType(), this);
                ThisConnection.SafeWrite(outBuffer);
            }
        }
    }
}
