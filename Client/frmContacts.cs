using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using Common.Messages;
using Common.Interfaces;
using Common.Connections;
using Common.Delegates;
using Common.Windows;
using System.Text.RegularExpressions;
using Common.Config;
using System.IO;
using Common.Enumerations;
using Telerik.WinControls.UI;
using System.Reflection;

namespace Client
{
    public partial class frmContacts : Form, IContactsWindow
    {
        private Connection ThisConnection = new Connection();
        private System.Threading.Thread mThread;
        private Dictionary<string, IContactsPlugin> Plugins = new Dictionary<string, IContactsPlugin>();

        public RadTreeView ContactsList
        {
            get
            {
                return treeContacts;
            }
        }

        public void DisplayContacts(Message_GetContacts.Contact[] ContactList)
        {
            //STOP THE LIST FROM PAINTING DURING THIS UPDATE AND REMOVE ALL CURRENT NODES
            this.ContactsList.BeginUpdate();
            this.ContactsList.Nodes.Clear();

            //INSERT ALL OF THE CONTACTS SINCE WE REMOVED THEM FROM THE LIST BECAUSE THIS IS A BRAND NEW MESSAGE
            InsertContacts(ContactList);

            //SORT THE LIST THEN ALLOW IT TO PAINT AGAIN
            this.ContactsList.Sort();
            this.ContactsList.EndUpdate();
        }

        private void InsertContacts(Message_GetContacts.Contact[] ContactList)
        {
            foreach (Message_GetContacts.Contact tmpContact in ContactList)
            {
                RadTreeNode LastNode = null;

                //APPEND AVAILABLE AND OFFLINE TO THE NODE'S GROUP DEPENDING ON IF ITS ONLINE / OFFLINE
                if (tmpContact.Online)
                    tmpContact.Group = "Available\\" + tmpContact.Group;
                else
                    tmpContact.Group = "Offline\\" + tmpContact.Group;

                //LOOP THROUGH ALL OF THE ITEMS IN THE CONTACTS GROUP TO FIND WHERE TO PLACE IT
                foreach (string parentNodeStr in tmpContact.Group.Split('\\'))
                {
                    RadTreeNode tmpNode;

                    //IF THIS IS THE FIRST TIME RUNNING THEN WE NEED TO SEARCH THE ROOT NODES ELSE ONLY SEARCH THE LAST NODE WE WERE INSIDE OF
                    if (LastNode == null)
                        tmpNode = this.ContactsList.Nodes.Find(parentNodeStr, false).FirstOrDefault();
                    else
                        tmpNode = LastNode.Nodes.Find(parentNodeStr, false).FirstOrDefault();

                    //IF THE NODE WAS NOT FOUND IT MEANS THE FOLDER HAS NOT BEEN CREATED AND WE NEED TO CREATE IT
                    if (tmpNode == null)
                    {
                        //CREATE THE NODE AND REMEMBER ITS NAME
                        tmpNode = new RadTreeNode(parentNodeStr);
                        tmpNode.Name = parentNodeStr;

                        //IF THIS IS A ROOT NODE THEN ADD IT TO THE ROOT ELSE ADD IT TO THE LASTNODE WE FOUND
                        if (LastNode == null)
                            this.ContactsList.Nodes.Add(tmpNode);
                        else
                            LastNode.Nodes.Add(tmpNode);
                    }

                    //REMEMBER THE LAST NODE SO WE KNOW WHERE TO ADD IT TO
                    LastNode = tmpNode;
                }

                //CREATE THE CONTACT NODE
                //THIS MIGHT SHOULD BE STORED IN THE DATABASE AS LAST KNOWN STATUS....  POSIBLY WHERE TO STORE ALL STATUS MESSAGES??
                RadTreeNode ContactNode = new RadTreeNode(tmpContact.DName + " - " + tmpContact.LastStatus);
                ContactNode.Name = tmpContact.UserID;
                ContactNode.Tag = tmpContact.DName;
                ContactNode.ForeColor = (tmpContact.Online ? Color.Blue : Color.Red);

                //ADD IT TO THE LIST OF CONTACTS AND MAKE IT VISIBLE IF THE USER IS ONLINE
                LastNode.Nodes.Add(ContactNode);
                if (tmpContact.Online)
                {
                    //TRAVERSE THE TREE AND EXPAND ALL OF THE NODES PARENTS SO IT IS VISIBLE TO THE USER
                    for (RadTreeNode tmpNode = ContactNode; tmpNode != null; tmpNode = tmpNode.PrevNode)
                        tmpNode.Expand();
                }
            }

            //SORT THE TREE AFTER THE UPDATE
            treeContacts.Sort();
        }

        public void UpdateStatus(Message_StatusUpdate newStatus)
        {
            RadTreeNode tmpNode = this.ContactsList.Nodes.Find(newStatus.UserID, true).FirstOrDefault();

            //IF WE FOUND A NODE THEN SETUP THE VARS FOR ADDING THIS NODE TO THE TREE IN THE CORRECT PLACE NOW
            if (tmpNode != null)
            {
                //TODO: ADD ABILITY FOR USER TO CHOOSE TO NOT BE NOTIFIED WHEN A USER SIGNS ON OR OFF
                //IF THE CONTACT WAS PREVIOUSLY OFFLINE AND ITS NOT THE CURRENT USER THEN SHOW A BALLOON TO THE USER
                if (tmpNode.FullPath.Contains("Offline") && tmpNode.Name != ThisConnection.UserID && newStatus.Online)
                {
                    //SETUP A BALLOON TO DISPLAY TO THE USER SO THEY KNOW SOMEONE HAS COME ONLINE
                    trayIcon.BalloonTipText = string.Format("{0} Has Just Come Online", (string)tmpNode.Tag);
                    trayIcon.BalloonTipTitle = "Contact Signed On";
                    trayIcon.BalloonTipIcon = ToolTipIcon.Info;

                    //SHOW THE BALLOON FOR 5 SECONDS SO THE USER CAN SEE IT
                    trayIcon.ShowBalloonTip(5000);
                }
                //IF THE CONTACT WAS PREVIOUSLY ONLINE AND ITS NOT THE CURRENT USER THEN SHOW A BALLOON TO THE USER LETTING THEM KNOW THE USER HAS GONE OFFLINE
                else if (tmpNode.FullPath.Contains("Available") && tmpNode.Name != ThisConnection.UserID && newStatus.Online == false)
                {
                    //SETUP A BALLOON TO DISPLAY TO THE USER SO THEY KNOW SOMEONE HAS COME ONLINE
                    trayIcon.BalloonTipText = string.Format("{0} Has Just Went Offline", (string)tmpNode.Tag);
                    trayIcon.BalloonTipTitle = "Contact Signed Off";
                    trayIcon.BalloonTipIcon = ToolTipIcon.Info;

                    //SHOW THE BALLOON FOR 5 SECONDS SO THE USER CAN SEE IT
                    trayIcon.ShowBalloonTip(5000);
                }

                //CREATE A NEW CONTACT ITEM, REMOVE IT FROM THE TREE, AND ADD THE NEW CONTACT WITH THE NEW STATUS
                //TODO: IF THE PARENT NODE IS EMPTY AFTER REMOVAL WE SHOULD PROBABLY REMOVE IT TOO
                Message_GetContacts.Contact tmpContact = new Message_GetContacts.Contact() { DName = (string)tmpNode.Tag, Group = tmpNode.FullPath.Substring(0, tmpNode.FullPath.LastIndexOf("\\")).Replace("Offline\\", "").Replace("Available\\", ""), Online = newStatus.Online, UserID = newStatus.UserID, LastStatus = newStatus.Status };
                tmpNode.Remove();

                //INSERT THE CONTACT BACK INTO THE TREE WITH THE NEW STATUS
                InsertContacts(new Message_GetContacts.Contact[] { tmpContact });
            }
        }

        public frmContacts()
        {
            //THIS IS THE WRONG WAY TO HAVE CALLED THIS FORM SO EXIT THE APPLICATION
            InitializeComponent();
            Application.Exit();
        }

        public frmContacts(string userName, string password)
        {
            InitializeComponent();
            Dictionary<string, string> ServerInfo = ConfigWrapper.GetSetting("server")[0];

            //CHANGE THE TITLE TO MATCH WHOS CONTACT WINDOW THIS IS
            this.Text = string.Format("{0}'s - Contact List", userName);

            //CREATE A CONNECTION AND OPEN IT TO THE SERVER
            ThisConnection.TcpConnection = new TcpClient();
            try
            {
                ThisConnection.TcpConnection.Connect(ServerInfo["host"], int.Parse(ServerInfo["port"]));
            }
            catch
            {
                //THIS WILL CATCH THE SERVER REFUSING CONNECTION BECAUSE THE SERVER IS NOT RUNNING OR THE SERVER IP IS INCORRECT
                //TODO: NEED TO TELL THE USER THIS HAS HAPPENED
            }

            //WAIT FOR THE CONNECTION TO CONNECT
            long StartTicks = DateTime.Now.Ticks;
            while (ThisConnection.TcpConnection.Connected == false && new TimeSpan(DateTime.Now.Ticks - StartTicks).Seconds <= 5)
                System.Threading.Thread.Sleep(10);

            if (ThisConnection.TcpConnection.Connected)
            {
                //IF THE CONNECTION WORKED THEN SHOW THIS FORM
                tmrKeepAlive.Enabled = true;

                //COPY THE STREAM VARIABLE TO THE CONNECTION FOR PASSING AROUND
                ThisConnection.TcpConnection.SendBufferSize = 1024 * 1024 * 20;
                ThisConnection.TcpConnection.ReceiveBufferSize = 1024 * 1024 * 20;
                ThisConnection.NetStream = ThisConnection.TcpConnection.GetStream();

                //SETUP THE READING THREAD TO RUN IN THE BACKGROUND WAITING ON DATA TO COME IN
                mThread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ReadThread));
                mThread.SetApartmentState(System.Threading.ApartmentState.STA);
                mThread.IsBackground = true;
                mThread.Start(ThisConnection);

                //HAND SHAKE
                new Message_HandShake().Send(ThisConnection);

                //NEED TO AUTHENTICATE AND WAIT FOR A RESPONSE
                new Message_Authenticate() { UserName = userName, Password = password }.Send(ThisConnection);
                while (ThisConnection.Authenticated == false && ThisConnection.TcpConnection.Connected)
                    Application.DoEvents();

                //GET THE USERS CONTACTS ONCE WE ARE CONNECTED
                new Message_GetContacts().Send(ThisConnection);

                //GET A LIST OF MISSED CONVERSATIONS
                if (ConfigWrapper.GetSetting("ConversationSettings").FirstOrDefault() == null || bool.Parse(ConfigWrapper.GetSetting("ConversationSettings").FirstOrDefault()["CheckMissedConversations"]) == true)
                    new Message_GetMissedConversations() { IsLoginCall = true }.Send(ThisConnection);
            }
            else
            {
                MessageBox.Show("Unable to contact server please try again later!");
                Globals.Exiting = false;
                this.Close();
            }

            //LOAD ALL CONTACT PLUGINS THE USER HAS
            CreatePluginList();

            //SHOW THE FORM NOW
            this.Show();
        }

        private void CreateWindow(string Sender, string ConversationID, ref Form NewWindow, WindowType WindowType)
        {
            Form tmpWindow = null;
            this.Invoke((MethodInvoker)delegate
            {
                switch (WindowType)
                {
                    case WindowType.Conversation:

                        //CREATE A NEW CONVERSATION WINDOW
                        tmpWindow = new frmConversation(ThisConnection, ConversationID);
                        tmpWindow.Show();
                        break;

                    case WindowType.RecentConversationList:

                        //CREATE A RECENT CONVERSATION LIST WINDOW AND HAND IT BACK
                        tmpWindow = new frmConversationList();
                        ((frmConversationList)tmpWindow).ThisConnection = ThisConnection;
                        tmpWindow.Show();
                        break;

                    case WindowType.MissedConversationList:

                        //CREATE A RECENT CONVERSATION LIST WINDOW AND HAND IT BACK
                        tmpWindow = new frmConversationList();
                        ((frmConversationList)tmpWindow).ThisConnection = ThisConnection;
                        tmpWindow.Text = "Missed Conversations";
                        tmpWindow.Show();
                        break;
                }
            });

            NewWindow = tmpWindow;
        }

        private void ReadThread(object State)
        {
            byte[] buffer = new byte[1024];
            StringBuilder tmpBuffer = new StringBuilder();

            //WHILE THE SERVER IS STILL CONNECTED CONTINUE TO LOOP
            while (ThisConnection.TcpConnection.Client.Connected)
            {
                try
                {
                    //READ FROM THE BUFFER AND APPEND IT TO OUR INPUT
                    int ReadLength = ThisConnection.NetStream.Read(buffer, 0, buffer.Length);
                    tmpBuffer.Append(System.Text.ASCIIEncoding.ASCII.GetChars(buffer, 0, ReadLength));

                    //IF WE DIDNT READ ANYTHING ITS BECAUSE THE CONNECTION WAS LOST
                    if (ReadLength == 0)
                        throw new System.IO.IOException();

                    //IF THE INPUT CONTAINS A HEADER AND A FOOTER THEN WE WILL TRY TO PARSE AND EXECUTE IT
                    if (tmpBuffer.ToString().Contains("PACKET_FOOTER"))
                    {
                        //USE REGEX TO SPLIT THE COMMAND INTO SEPERATE COMMANDS BASED ON THE HEADER AND FOOTER AS A SEPERATOR
                        string command = Regex.Split(tmpBuffer.ToString(), "PACKET_FOOTER")[0] + "PACKET_FOOTER";

                        //TRY TO UNWRAP THE MESSAGE AND EXECUTE IT
                        try
                        {
                            IServerMessage receivedMessage = (IServerMessage)MessageWrapper.UnPackageFromTCP(System.Text.ASCIIEncoding.ASCII.GetBytes(command.Replace("PACKET_HEADER", "").Replace("PACKET_FOOTER", "")), true);
                            receivedMessage.ClientSide(Application.OpenForms, new CreateClientWindow(CreateWindow), ThisConnection);

                            //REMOVE THE COMMAND WE EXECUTED FROM THE BUFFER
                            tmpBuffer = tmpBuffer.Replace(command, "");
                        }
                        catch (Exception ex)
                        {
                            //IF THERE WAS AN ERROR WE USE THE ERROR FORM FROM COMMON AND DISPLAY IT TO THE USER
                            this.Invoke((MethodInvoker)delegate
                            {
                                frmError tmpError = new frmError();
                                tmpError.txtDetails.Text = string.Format("{0}\n\n\nBuffer Dump: {1}", ex.ToString(), tmpBuffer.ToString());
                                tmpError.Show();
                            });

                            //CLEAR THE BUFFER BECAUSE WE NO LONGER KNOW IF IT IS VALID OR NOT
                            tmpBuffer = new StringBuilder();
                            while (ThisConnection.NetStream.DataAvailable)
                            {
                                ThisConnection.NetStream.ReadByte();
                            }
                        }
                    }
                }
                catch (System.IO.IOException)
                {
                    //THIS IS BECAUSE THE CONNECTION WAS CLOSED BY THE SERVER
                    MessageBox.Show("Connection to server lost!");

                    break;
                }

                //IF THE BUFFER IS CLEAR THEN WAIT A LITTLE BEFORE LOOPING OTHER WISE GO BACK QUICKLY AND CHECK FOR MORE DATA
                if (tmpBuffer.Length == 0)
                    System.Threading.Thread.Sleep(10);
            }

            //THIS IS BECAUSE THE CONNECTION WAS CLOSED BY THE SERVER
            if (!Globals.Exiting)
            {
                this.Invoke((MethodInvoker)delegate { this.Close(); });
            }
        }

        private void CreatePluginList()
        {
            foreach (FileInfo tmpDLL in new DirectoryInfo(Path.Combine(Application.StartupPath, "Plugins")).GetFiles("*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    Assembly tmpAssembly = System.Reflection.Assembly.LoadFrom(tmpDLL.FullName);
                    foreach (Type tmpType in tmpAssembly.GetTypes())
                    {
                        try
                        {
                            IContactsPlugin tmpPlugin = (IContactsPlugin)tmpAssembly.CreateInstance(tmpType.FullName);
                            ToolStripMenuItem tmpMenuItem = tmpPlugin.Initialize(this, ThisConnection);

                            //ADD THE MENU ITEM TO THE DROP DOWN LIST
                            if (tmpMenuItem != null)
                            {
                                extraPluginsToolStripMenuItem.DropDownItems.Add(tmpMenuItem);
                                extraPluginsToolStripMenuItem.Visible = true;
                            }

                            //ADD THE PLUGIN TO THE LIST OF LOADED PLUGINS
                            Plugins.Add(tmpPlugin.Name, tmpPlugin);
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SHOW AN ABOUT BOX TO THE USER
            new frmAbout().ShowDialog();
        }

        private void recentConversationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GET A LIST OF ALL RECENT CONVERSATIONS FOR THIS USER
            new Message_GetRecentConversations() { DaysToSearch = 14 }.Send(ThisConnection);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //WE ARE REALLY EXITING SO KEEP TRACK OF THAT SO OUR OUTTER PROGRAM LOOP WILL NOT CONTINUE
            Globals.Exiting = true;
            this.Close();
        }

        private void tmrKeepAlive_Tick(object sender, EventArgs e)
        {
            //JUST SEND A KEEP ALIVE PACKET EVERY 60 SECONDS
            new Message_KeppAlive().Send(ThisConnection);
        }

        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //SHOW THE CONTACTS FORM
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Focus();
        }

        private void frmContacts_Resize(object sender, EventArgs e)
        {
            //IF THE USER MINIMIZED THE WINDOW THEN SEND IT TO THE SYSTEM TRAY
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void sendIMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //JUST SIMULATE A DOUBLE CLICK ON A NODE SO WE CAN RUN THE SAME SEND ROUTINE
            treeContacts_DoubleClick(null, null);
        }

        private void frmContacts_FormClosing(object sender, FormClosingEventArgs e)
        {
            //CHECK TO SEE WHY WE ARE CLOSING AND SEE IF WE NEED TO CANCEL THE CLOSE AND JUST HIDE OR REALLY EXIT
            if (e.CloseReason == CloseReason.UserClosing && Globals.Exiting)
            {
                //REMOVE THE TRAY ICON FROM THE TRAY BECAUSE THIS WINDOW IS REALLY CLOSING
                trayIcon.Visible = false;
                Globals.Exiting = true;

                //UNLOAD ALL OF THE PLUGINS
                foreach (IContactsPlugin tmpPlugin in Plugins.Values)
                    try
                    {
                        tmpPlugin.Dispose();
                    }
                    catch
                    {
                    }

                Application.Exit();
            }
            else if (e.CloseReason == CloseReason.UserClosing && !Globals.Exiting && ThisConnection.Authenticated && ThisConnection.TcpConnection.Connected)
            {
                //JUST HIDE THE WINDOW AND DONT EXIT
                e.Cancel = true;
                this.Hide();
            }
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //JUST CALL THE SAME DOUBLE CLICK WHERE THE REST OF THE CODE IS
            trayIcon_MouseDoubleClick(null, null);
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //CALL THE MAIN MENU EXIT ITEM SO IT CAN EXIT FOR US
            exitToolStripMenuItem_Click(null, null);
        }

        //TODO: ADD OTHER OPTIONS AND ALLOW USERS TO CREATE CUSTOM AWAY MESSAGES WHICH CAN BE SAVED

        private void awayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UPDATE OUR STATUS TO AWAY
            awayToolStripMenuItem.Checked = true;
            activeToolStripMenuItem.Checked = false;
            outToLunchToolStripMenuItem.Checked = false;
            new Message_StatusUpdate() { Online = true, Status = "Away", UserID = ThisConnection.UserID }.Send(ThisConnection);
        }

        private void activeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UPDATE OUR STATUS TO ACTIVE
            awayToolStripMenuItem.Checked = false;
            activeToolStripMenuItem.Checked = true;
            outToLunchToolStripMenuItem.Checked = false;
            new Message_StatusUpdate() { Online = true, Status = "Active", UserID = ThisConnection.UserID }.Send(ThisConnection);
        }

        private void outToLunchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UPDATE OUR STATUS TO ACTIVE
            awayToolStripMenuItem.Checked = false;
            activeToolStripMenuItem.Checked = false;
            outToLunchToolStripMenuItem.Checked = true;
            new Message_StatusUpdate() { Online = true, Status = "Out To Lunch", UserID = ThisConnection.UserID }.Send(ThisConnection);
        }

        private void getRunningProcessesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LOOP THROUGH EACH CONTACT SELECTED AND SEND A GET RUNNING PROCESSES MESSAGE
            foreach (RadTreeNode tmpNode in (from a in this.treeContacts.SelectedNodes where a.Tag != null select a))
            {
                new Message_GetRunningProcesses() { UserID = tmpNode.Name, Sender = ThisConnection.UserID }.Send(ThisConnection);
            }
        }

        private void getScreenShotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LOOP THROUGH EACH CONTACT SELECTED AND SEND A GET SCREEN SHOT MESSAGE
            foreach (RadTreeNode tmpNode in (from a in this.treeContacts.SelectedNodes where a.Tag != null select a))
            {
                new Message_GetScreenShot() { UserID = tmpNode.Name, Sender = ThisConnection.DisplayName }.Send(ThisConnection);
            }
        }

        private void treeContacts_DoubleClick(object sender, EventArgs e)
        {
            //CREATE THE LIST OF RECIPEINTS WHO SHOULD RECEIVE THIS MESSAGE
            if (treeContacts.SelectedNodes.Where(a => a.Tag != null).Count() > 0)
            {
                List<string> Members = (from a in treeContacts.SelectedNodes where a.Tag != null select (string)a.Name).ToList();
                Members.Add(ThisConnection.UserID);

                //SEND THE MESSAGE
                new Message_CreateConversation() { ConversationID = Guid.NewGuid().ToString(), ConversationMembers = Members.ToArray() }.Send(ThisConnection);
            }
        }

        private void missedConversationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CREATE A NEW GET MISSED CONVERSATIONS MESSAGE AND SEND IT TO THE SERVER
            new Message_GetMissedConversations() { IsLoginCall = false }.Send(ThisConnection);
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSettings().Show();
        }
    }
}
