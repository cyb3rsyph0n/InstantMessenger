using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using System.Windows.Forms;
using Common.Delegates;
using System.IO;
using System.Drawing;
using Common.Windows;

namespace Common.Messages
{
    public class Message_GetScreenShotResponse : IServerMessage
    {
        public string UserID { get; set; }
        public string Sender { get; set; }
        public byte[][] ImageData { get; set; }

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            this.Sender = StaticFunctions.GetContactsProvider().UserNameFromID(ThisConnection.UserID);
            foreach (Connection serverConnection in ServerConnections)
                if (serverConnection.UserID == this.UserID)
                    Send(serverConnection);
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            foreach (byte[] tmpImage in ImageData)
            {
                using (MemoryStream tmpMemStream = new MemoryStream(tmpImage))
                {
                    using (Image tmpBitmap = Bitmap.FromStream(tmpMemStream))
                    {
                        frmScreenShot tmpScreenShot = new frmScreenShot();
                        tmpScreenShot.Text = string.Format("Screen Shot from {0}", this.Sender);
                        tmpScreenShot.picScreenShot.Image = (Image)tmpBitmap.Clone();
                        tmpScreenShot.Show();
                    }
                }
            }
        }
    }
}
