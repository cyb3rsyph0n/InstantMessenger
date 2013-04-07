using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using System.Windows.Forms;
using Common.Delegates;
using Common.DBAccess;
using System.IO;
using System.Security.Permissions;
using System.Diagnostics;

namespace Common.Messages
{
    public class Message_GetFile : IServerMessage
    {
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
        public string FileID { get; set; }
        public string SaveLocation { get; set; }

        public override void ServerSide(List<Connection> ServerConnections, Connection ThisConnection)
        {
            using (FilesDataContext tmpDB = new FilesDataContext())
            {
                Message_GetFile tmpReturn = (from a in tmpDB.FileItems where a.FileID == this.FileID select new Message_GetFile(){ FileBytes = a.FileBytes.ToArray(), FileName = a.FileName, SaveLocation = this.SaveLocation}).FirstOrDefault();

                if(tmpReturn != null)
                    tmpReturn.Send(ThisConnection);
            }
        }

        [STAThread]
        public override void ClientSide(FormCollection OpenWindows, CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            //CLOSE THE STATUS WINDOW ONCE THE FILE GET IS COMPLETE
            foreach (Form OpenWindow in OpenWindows)
                if (OpenWindow.Text == "Get Status")
                {
                    OpenWindow.Invoke((MethodInvoker)delegate { OpenWindow.Close(); });
                    break;
                }

            //IF THE USER SELECTED A DOWNLOAD LOCATION THEN SAVE THE FILE, OTHERWISE OPEN WITH INTERNET EXPLORER
            if (string.IsNullOrEmpty(this.SaveLocation))
            {
                string tmpFileName = Path.Combine(Path.GetTempPath(), this.FileName);
                File.WriteAllBytes(tmpFileName, this.FileBytes);

                //CREATE AN IE PROCESS AND LAUNCH IT WITH THE FILE AS THE PARAMETER
                Process tmpProc = new Process();
                tmpProc.StartInfo = new ProcessStartInfo("IExplore.exe", tmpFileName);
                tmpProc.Start();
            }
            else
            {
                //SAVE THE FILE TO THE SAVE LOCATION AND LET THE USER KNOW IT IS DONE
                File.WriteAllBytes(this.SaveLocation, this.FileBytes);
                MessageBox.Show("File Download Complete");
            }

        }
    }
}
