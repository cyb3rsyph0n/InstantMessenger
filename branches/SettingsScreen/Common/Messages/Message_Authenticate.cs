using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Connections;
using System.IO;
using System.Windows.Forms;
using Common.Config;
using Common.Structures;

namespace Common.Messages
{
    public class Message_Authenticate : IServerMessage
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public bool Success { get; set; }

        public override void ServerSide(List<Common.Connections.Connection> ServerConnections, Common.Connections.Connection ThisConnection)
        {
            IAuthenticationProvider AuthProvider = StaticFunctions.GetAuthenticationProvider();

            //GET THE AUTHENTICATION RESULT FROM THE AUTHENTICATE FUNCTION
            AuthResult tmpResult = AuthProvider.Authenticate(UserName, Password);

            //DOUBLE CHECK TO MAKE SURE THAT THE USERID WE GOT BACK IS NOT ALREADY IN USE
            if ((from a in ServerConnections where a.UserID == tmpResult.UserID && a != ThisConnection select a).Count() != 0)
            {
                //THROW AN ERROR BECAUSE A USER CAN NOT BE LOGGED IN TWICE
                new Message_Error() { Message = "Username cannot be logged in twice!" }.Send(ThisConnection);
                tmpResult.Success = false;

                //CLOSE THE CONNECTION SO THE USER MAY LOGIN THE NEXT TIME
                //TODO: SHOULD PROVIDE THIS CONNECTION A METHOD OF SAYING NOT TO LOG THEM OFF OR ATLEAST LETTING THEM KNOW WHY THEY ARE BEING LOGGED OFF
                (from a in ServerConnections where a.UserID == tmpResult.UserID && a != ThisConnection select a).First().TcpConnection.Close();
            }

            //STORE THE RESULT FROM THE AUTHENTICATION CALL
            if (tmpResult.Success)
            {
                ThisConnection.UserID = tmpResult.UserID;
                ThisConnection.Authenticated = true;
                this.Success = tmpResult.Success;
                this.UserID = tmpResult.UserID;
                this.DisplayName = tmpResult.DisplayName;
            }

            //SEND THE RESPONSE BACK TO THE CLIENT
            Send(ThisConnection);
        }

        public override void ClientSide(System.Windows.Forms.FormCollection OpenWindows, Common.Delegates.CreateClientWindow CreateWindow, Connection ThisConnection)
        {
            //IF WE GET BACK A SUCCESS WE STORE THIS CONNECTION AS AUTHENTICATED
            if (this.Success)
            {
                ThisConnection.UserID = this.UserID;
                ThisConnection.DisplayName = this.DisplayName;
                ThisConnection.Authenticated = true;
                new Message_StatusUpdate() { Online = true, UserID = this.UserID, Status = "Active" }.Send(ThisConnection);
            }
            else
            {
                MessageBox.Show("Authentication Failed!");
                ThisConnection.TcpConnection.Close();
            }
        }

        public override void Send(Connection ThisConnection)
        {
            base.Send(ThisConnection, true);
        }
    }
}
