using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using System.Windows.Forms;
using Common.Delegates;
using System.Diagnostics;
using Common.Windows;

namespace Common.Messages
{
    public class Message_GetRunningProcessesResponse : IServerMessage
    {
        public RunningProcess[] RunningProcesses { get; set; }
        public string UserID { get; set; }
        public string Sender { get; set; }

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            this.Sender = StaticFunctions.GetContactsProvider().UserNameFromID(ThisConnection.UserID);
            foreach (Connection serverConnection in ServerConnections)
            {
                if (serverConnection.UserID == this.UserID)
                    Send(serverConnection);
            }
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            //CREATE A NEW FRMPROCESS TO DISPLAY THE RUNNING PROCESSES ON THE USERS COMPUTER
            frmRunningProcesses tmpFrm = new frmRunningProcesses();

            //BIND THE DATASOURCE TO THE RETURN VALUE AND DISPLAY IT TO THE USER WHO REQUESTED IT
            tmpFrm.ThisConnection = ThisConnection;
            tmpFrm.gridRunningProcesses.DataSource = this.RunningProcesses;
            tmpFrm.Text = string.Format("Running Processes from {0}", this.Sender);
            tmpFrm.Show();
        }

        //CLASS TO HOLD INFORMATION ABOUT RUNNING PROCESSES
        public class RunningProcess
        {
            public int PID { get; set; }
            public string ProcessName { get; set; }
            public long MemoryUsage { get; set; }
            public int ThreadCount { get; set; }
        }
    }
}
