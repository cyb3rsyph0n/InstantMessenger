using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using Common.Delegates;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Common.Messages
{
    public class Message_GetScreenShot : IServerMessage
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

                    break;
                }
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            byte[][] ImageData = GetScreenShot();
            if(MessageBox.Show(string.Format("{0} Is requesting a screen shot of your desktop.  Do you want to allow them to view your screen?", this.Sender), "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
                new Message_GetScreenShotResponse() { ImageData = ImageData, UserID = this.UserID }.Send(ThisConnection);
        }

        private byte[][] GetScreenShot()
        {
            List<byte[]> tmpReturn = new List<byte[]>();

            foreach (Screen tmpScreen in Screen.AllScreens)
            {
                using (MemoryStream tmpMemStream = new MemoryStream())
                {
                    using (Bitmap tmpBitmap = new Bitmap(tmpScreen.Bounds.Width, tmpScreen.Bounds.Height))
                    {
                        using (Graphics tmpGraphics = Graphics.FromImage(tmpBitmap))
                        {
                            tmpGraphics.CopyFromScreen(new Point(tmpScreen.Bounds.Left, tmpScreen.Bounds.Top), new Point(0, 0), new Size(tmpScreen.Bounds.Width, tmpScreen.Bounds.Height), CopyPixelOperation.SourceCopy);
                            tmpBitmap.Save(tmpMemStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            tmpReturn.Add(tmpMemStream.ToArray());
                        }
                    }
                }
            }

            return tmpReturn.ToArray();
        }
    }
}