using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using System.Windows.Forms;
using Common.Delegates;
using Common.DBAccess;
using System.Security.Cryptography;

namespace Common.Messages
{
    public class Message_SendFile : IServerMessage
    {
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
        public string FileID { get; set; }
        public string ConversationID { get; set; }
        public string Sender { get; set; }
        public string SenderID { get; set; }

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            using (FilesDataContext tmpDB = new FilesDataContext())
            {
                FileItem tmpFile = new FileItem() { FileBytes = this.FileBytes, FileID = this.FileID, FileName = this.FileName, UploadDate = DateTime.Now, FileHash = ComputeHash(this.FileBytes) };
                tmpDB.FileItems.InsertOnSubmit(tmpFile);
                tmpDB.SubmitChanges();
            }

            //ZERO OUT THE FILE WHICH WAS SENT SO WE DONT SEND IT BACK TO THE USER FOR NO REASON AND RESPOND TO THE PERSON WHO SENT THIS MESSAGE THAT IT WORKED
            this.FileBytes = null;
            Send(ThisConnection);
        }

        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            new Message_Private(){ ConversationID=this.ConversationID, Sender=this.Sender, SenderID = this.SenderID, Message=string.Format(@"Has sent you a file: <a href=""getfile://{0}"">{1}</a>", this.FileID, this.FileName)}.Send(ThisConnection);

            foreach (Form OpenWindow in OpenWindows)
                if (OpenWindow.Text == "Send Status")
                {
                    OpenWindow.Invoke((MethodInvoker)delegate { OpenWindow.Close(); });
                    break;
                }
        }

        private string ComputeHash(byte[] p)
        {
            MD5 tmpCheckSum = MD5.Create();
            return BitConverter.ToString(tmpCheckSum.ComputeHash(p));
        }

    }
}