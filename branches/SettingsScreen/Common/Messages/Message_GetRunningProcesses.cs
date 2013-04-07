using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using System.Data;
using Common.Connections;
using Common.Delegates;
using System.Windows.Forms;
using System.Diagnostics;

namespace Common.Messages
{
    public class Message_GetRunningProcesses : IServerMessage
    {
        public string UserID { get; set; }
        public string Sender { get; set; }

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            foreach (Connection serverConnection in ServerConnections)
                if (serverConnection.UserID == this.UserID)
                {
                    this.UserID = ThisConnection.UserID;
                    Send(serverConnection);
                }
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            if (MessageBox.Show(string.Format("{0} Is requesting a list of all running processes on your system.  Do you want to allow them to view your processes?", this.Sender), "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
                new Message_GetRunningProcessesResponse() { RunningProcesses = (from a in Process.GetProcesses()select new Message_GetRunningProcessesResponse.RunningProcess { ProcessName = a.ProcessName, ThreadCount = a.Threads.Count, MemoryUsage = a.PrivateMemorySize64, PID=a.Id }).ToArray(), UserID = this.UserID }.Send(ThisConnection);
        }
    }
}
