using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using System.Windows.Forms;
using Common.Delegates;
using System.Diagnostics;

namespace Common.Messages
{
    public class Message_KillProcess : IServerMessage
    {
        public string UserID { get; set; }
        public int PID { get; set; }

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            foreach (Connection serverConnection in ServerConnections)
            {
                if (serverConnection.UserID == this.UserID)
                    Send(serverConnection);
            }
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            Process tmpProcess = (from a in Process.GetProcesses() where a.Id == PID select a).FirstOrDefault();

            if (tmpProcess != null)
                tmpProcess.Kill();
        }
    }
}
