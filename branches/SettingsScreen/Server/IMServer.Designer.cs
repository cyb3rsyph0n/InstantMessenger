using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using Common.Connections;
using Common.Messages;
using Common.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common.Config;
using System.IO;
using System.Linq;

namespace Server
{
    partial class IMServer
    {
        private List<Connection> tcpConnections = new List<Connection>();

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ReadThread(object State)
        {
            //HOLD THE PARAMETER COMING IN AS A CONNECTION
            Connection ThisConnection = (Connection)State;

            //USED TO HOLD THE INCOMING DATA UNTIL WE HAVE A COMPLETE PACKAGE
            StringBuilder tmpBuffer = new StringBuilder();
            byte[] buffer = new byte[1024];

#if DEBUG
            Console.WriteLine("Accepting Connection");
#endif

            //WHILE THERE IS A CONNECTION LISTENTING (BASICALLY INFINITE UNTIL A DISCONNECT CAUSES AN ERROR)
            while (ThisConnection.TcpConnection.Connected)
            {
                try
                {
                    //TRY TO READ FROM THE BUFFER AND APPEND IT TO THE OUTPUT
                    int readLength = ThisConnection.NetStream.Read(buffer, 0, buffer.Length);
                    tmpBuffer.Append(System.Text.ASCIIEncoding.ASCII.GetChars(buffer, 0, readLength));

                    //TEST CODE
                    if (readLength == 0)
                        throw new System.IO.IOException();

                    //CHECK TO SEE IF WE HAVE A COMPLETE MESSAGE OR NOT AND IF SO PARSE IT AND EXECUTE IT
                    if (tmpBuffer.ToString().Contains("PACKET_FOOTER"))
                    {
#if DEBUG
                        Console.Write(string.Format("Received Command: {0}", !string.IsNullOrEmpty(ThisConnection.UserID) ? StaticFunctions.GetContactsProvider().UserNameFromID(ThisConnection.UserID) + " " : ""));
#endif

                        try
                        {
                            //PARSE THE COMMAND USING REGEX
                            string command = Regex.Split(tmpBuffer.ToString(), "PACKET_FOOTER")[0] + "PACKET_FOOTER";

                            //PARSE THE MESSAGE AND DE-SERIALIZE IT INTO A CLASS FOR EXECUTION
                            IServerMessage receivedMessage = (IServerMessage)MessageWrapper.UnPackageFromTCP(System.Text.ASCIIEncoding.ASCII.GetBytes(command.Replace("PACKET_HEADER", "").Replace("PACKET_FOOTER", "")), true);

#if DEBUG
                            MessageWrapper tmpThingy = (MessageWrapper)MessageWrapper.UnPackageFromTCP(System.Text.ASCIIEncoding.ASCII.GetBytes(command.Replace("PACKET_HEADER", "").Replace("PACKET_FOOTER", "")), false);
                            Console.WriteLine(tmpThingy.MessageType);
#endif

                            //EXECUTE THE SERVER SIDE COMMAND BECAUSE WE ARE ON THE SERVER
                            receivedMessage.ServerSide(tcpConnections, ThisConnection);

                            //CLEAR THE BUFFER IN PREPERATION FOR THE NEW COMMAND
                            //tmpBuffer = new StringBuilder();
                            tmpBuffer = tmpBuffer.Replace(command, "");
                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            Console.WriteLine("Clearing Receive Buffer");
#endif

                            //MAKE SURE WE CLEAR THE INPUT BUFFER TO MAKE SURE WE START FROM SCRATCH WITH A CLEAR BUFFER
                            while (ThisConnection.NetStream.DataAvailable)
                            {
                                ThisConnection.NetStream.ReadByte();
                            }

                            //SEND THE ERROR BACK TO THE CLIENT SO THEY KNOW A SERVER ERROR OCCURED
                            Message_Error tmpError = new Message_Error() { Message = string.Format("Server Error:\n{0}\n\n\nBuffer Dump:\n{1}", ex.ToString(), tmpBuffer.ToString()) };
                            tmpError.Send(ThisConnection);

                            //CLEAR THE BUFFER NOW
                            tmpBuffer = new StringBuilder();
                        }
                    }
                }
                catch (System.IO.IOException)
                {
                    //IF THERE WAS AN ERROR AS A RESULT OF A NETWORK IO EXCEPTION
                    tcpConnections.Remove(ThisConnection);
                    ThisConnection.TcpConnection.Close();

                    //LET ALL USERS WHO BELONG TO THIS USERS CONTACT LIST KNOW THIS USER HAS GONE OFFLINE
                    new Message_StatusUpdate() { Status = "Offline", Online = false, UserID = ThisConnection.UserID }.ServerSide(tcpConnections, ThisConnection);

                    //EXIT THE LOOP
                    break;
                }

                //REST FOR 10 MILISECONDS WHILE OTHER THREADS EXECUTE
                Application.DoEvents();
                if (tmpBuffer.Length == 0)
                    System.Threading.Thread.Sleep(10);
            }
#if DEBUG
            Console.WriteLine("Connection Closed");
#endif
        }

        public void ListenThread()
        {
            Dictionary<string, string> ListenInfo = ConfigWrapper.GetSetting("listen")[0];
            TcpListener tcpServer = new TcpListener(System.Net.IPAddress.Any, int.Parse(ListenInfo["port"]));

            //START THE LISTENING PORT
            tcpServer.Start();
#if DEBUG
            Console.WriteLine("Listening for connections");
#endif

            //JUST LOOP UNTIL THE PROGRAM IS CLOSED AND TRY TO ACCEPT CONNECTIONS
            while (true)
            {
                //CREATE A NEW THREAD AND CONNECTION HOLDER IN PREPERATION FOR THE INCOMING CONNECTION
                Connection tmpConnection = new Connection();
                Thread tmpThread = new Thread(new ParameterizedThreadStart(ReadThread));

                //WAIT UNTIL THERE IS A CONNECTION TO ACCEPT AND THEN ACCEPT IT
                tmpConnection.TcpConnection = tcpServer.AcceptTcpClient();

                //WAIT FOR THE CONNECTION TO SUCCEED BEFORE PROCEEDING
                while (tmpConnection.TcpConnection.Connected == false)
                {
                    System.Threading.Thread.Sleep(10);
                }

                //SETUP THE CONNECTION ITEMS FOR LATER USES
                tmpConnection.TcpConnection.SendBufferSize = 1024 * 1024 * 20;
                tmpConnection.TcpConnection.ReceiveBufferSize = 1024 * 1024 * 20;
                tmpConnection.NetStream = tmpConnection.TcpConnection.GetStream();
                tcpConnections.Add(tmpConnection);

                //SETUP THE THREAD TO RUN IN THE BACKGROUND SO IF THE SERVER EXITS ALL CONNECTION THREADS EXIT TOO
                tmpThread.IsBackground = true;

                //START THE READING THREAD
                tmpThread.Start(tmpConnection);
            }
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "IMService";
        }

        #endregion
    }
}
