using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Messages;
using Common.Delegates;
using Common.Interfaces;
using Common.Connections;
using Common.Windows;

namespace Common.Messages
{
    public class Message_Error : IServerMessage
    {
        public string Message { get; set; }

        #region IServerMessage Members

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            Send(ThisConnection);
        }

        public override void ClientSide(System.Windows.Forms.FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            frmError tmpError = new frmError();
            tmpError.txtDetails.Text = string.Format("{0}", this.Message);
            tmpError.ShowDialog();
        }

        public override void Send(Connection ThisConnection)
        {
            base.Send(ThisConnection, true);
        }

        #endregion
    }
}
