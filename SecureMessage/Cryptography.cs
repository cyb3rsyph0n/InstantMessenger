using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces;
using Common.Enumerations;
using Common.Connections;
using System.Windows.Forms;
using Common.Messages;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Drawing;

namespace SecureMessage
{
    public class Cryptography : IConversationPlugin
    {
        private ToolStripMenuItem MainMenu = null;
        private ToolStripMenuItem EnabledMenu = null;
        private ToolStripMenuItem ShowDebugMenu = null;

        private IConversationWindow Window = null;
        private Form frmWindow = null;
        private Connection Connection = null;
        private byte[] MyPrivateKey;
        private byte[] MyPublicKey;
        private Dictionary<string, byte[]> PublicKeys = new Dictionary<string, byte[]>();
        private frmDebug DebugWindow = null;

        private bool HasSentKey = false;

        public string Name
        {
            //RETURN THE PLUGINS NAME WHICH SHOULD BE SOMETHING DESCRIPTIVE
            get { return "Secure Message"; }
        }

        public Version VersionInfo
        {
            //RETURN BASIC VERSION INFORMATION
            get { return new Version(1, 0); }
        }

        public PluginType PluginType
        {
            //RETURN THAT OUR PLUGIN TYPE IS THAT OF A CONVERSATION THIS IS PURELY FOR LATER USE WHEN FILES ARE BROUGHT INTO THIS MIX
            get { return PluginType.Conversation; }
        }

        public bool SupportsSending
        {
            //RETURN THAT THIS PLUGIN SUPPORTS SENDING A MESSAGE
            get { return true; }
        }

        public bool SupportsReceiving
        {
            //RETURN THAT THIS PLUGIN SUPOPRTS RECEIVING
            get { return true; }
        }

        public int SendOrder
        {
            //MAKE SURE THIS RUNS AFTER EVERYTHING SO ENCRYPTION IS THE LAST THING THAT HAPPENS
            get { return int.MaxValue; }
        }

        public int ReceiveOrder
        {
            //MAKE SURE THIS RUNS BEFORE ANY OTHER PLUGIN BECAUSE NOTHING WIILL WORK OTHERWISE
            get { return int.MinValue; }
        }

        private Control mPropertyPage = null;
        public Control PropertyPage
        {
            get
            {
                if (mPropertyPage == null)
                    mPropertyPage = new propertyPage();

                return mPropertyPage;
            }
        }

        public Image PropertyPageImage
        {
            get
            {
                return Properties.Resources._lock;
            }
        }

        public ToolStripMenuItem Initialize(Form Window, Connection ThisConnection)
        {
            //SAVE THE INFORMATION WE HAVE BEEN GIVEN FOR LATER USE
            this.Connection = ThisConnection;
            this.frmWindow = Window;
            this.Window = (IConversationWindow)Window;

            //CREATE MENU ITEMS
            CreateMenuItems();

            //CHECK TO MAKE SURE THE KEYS EXISTS AND IF THEY DO THEN IMPORT THEM OTHERWISE CREATE THEM THEN IMPORT THEM
            CheckForPersonalKeyPair(this.Window.ConversationID);

            //RETURN THE MENU ITEM WE CREATED
            return this.MainMenu;
        }

        private void CreateMenuItems()
        {
            this.MainMenu = new ToolStripMenuItem(string.Format("{0} v{1}.{2}", this.Name, this.VersionInfo.Major, this.VersionInfo.Minor));
            EnabledMenu = new ToolStripMenuItem("&Enabled");
            ShowDebugMenu = new ToolStripMenuItem("&Show Debug Window");
            EnabledMenu.CheckedChanged += new EventHandler(EnabledSecureMessage_CheckedChanged);
            EnabledMenu.CheckOnClick = true;
            ShowDebugMenu.Click += new EventHandler(ShowDebugWindow_Click);

            //ADD THE TWO MENU ITEMS TO THE ROOT ITEM
            this.MainMenu.DropDownItems.Add(EnabledMenu);
            this.MainMenu.DropDownItems.Add(new ToolStripSeparator());
            this.MainMenu.DropDownItems.Add(ShowDebugMenu);

        }

        void EnabledSecureMessage_CheckedChanged(object sender, EventArgs e)
        {
            if (EnabledMenu.Checked)
            {
                this.frmWindow.Icon = Icon.FromHandle(Properties.Resources._lock.GetHicon());

                if (!HasSentKey)
                {
                    //SEND THE PRIVATE MESSAGE THAT STATES WE ARE GOING TO ESTABLISH A PRIVATE COMMUNICATION
                    new Message_Private() { ConversationID = this.Window.ConversationID, Message = string.Format("Begin Key ---->{0}<---- End Key", Convert.ToBase64String(this.MyPublicKey)), Sender = this.Connection.DisplayName, SenderID = this.Connection.UserID }.Send(this.Connection);
                    HasSentKey = true;
                }
            }
        }

        void ShowDebugWindow_Click(object sender, EventArgs e)
        {
            if (DebugWindow == null || DebugWindow.Disposing || DebugWindow.IsDisposed)
            {
                DebugWindow = new frmDebug();
                DebugWindow.Text = string.Format("Debugging Conversation - {0}", this.Window.ConversationID);
            }

            DebugWindow.Show();
        }

        private void CheckForPersonalKeyPair(string ConversationID)
        {
            string AppDataPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Instant Messenger\\" + Application.ProductName + "\\KeyStore\\" + ConversationID);
            string PrivateKeyPath = Path.Combine(AppDataPath, "Private.key");
            string PublicKeyPath = Path.Combine(AppDataPath, "Public.key");

            //MAKE SURE THE DIRECTORY EXISTS BEFORE WE TRY TO CREATE FILES THERE
            Directory.CreateDirectory(AppDataPath);

            //IF THE KEYS DO NOT EXISTS THEN CREATE A NEW PAIR
            if (!File.Exists(PrivateKeyPath) || !File.Exists(PublicKeyPath))
            {
                using (RSACryptoServiceProvider tmpKey = new RSACryptoServiceProvider(1024))
                {
                    //WRITE BOTH THE PRIVATE AND PUBLIC KEY TO DISK
                    File.WriteAllBytes(PrivateKeyPath, tmpKey.ExportCspBlob(true));
                    File.WriteAllBytes(PublicKeyPath, tmpKey.ExportCspBlob(false));
                }
            }
            else
            {
                this.HasSentKey = true;
            }

            //READ THE TWO KEY FILES IN
            this.MyPrivateKey = File.ReadAllBytes(PrivateKeyPath);
            this.MyPublicKey = File.ReadAllBytes(PublicKeyPath);
        }

        public void OutgoingMessage(ref Message_Private OutgoingMessage, ref bool ContinueToSend)
        {
            if (this.EnabledMenu.Checked)
            {
                //ENCRYPT THE MESSAGE WITH THE KEYS WE HAVE BEEN PROVIDED
                EncryptedWrapper tmpMessage = EncryptedWrapper.EncryptMessage(OutgoingMessage.Message, this.PublicKeys);
                OutgoingMessage.Message = Convert.ToBase64String(EncryptedWrapper.Serialize(tmpMessage));
            }
        }

        public void IncomingMessage(ref Message_Private IncomingMessage, ref bool DisplayMessage)
        {
            //IF THERE IS A DEBUG WINDOW THEN UPDATE THE TEXT
            if (DebugWindow != null)
                DebugWindow.txtMessages.Text += string.Format("Incoming Message: {0} - {1}\n\n", IncomingMessage.TimeStamp.ToString("m/d/yy HH:mm:ss"), IncomingMessage.Message);

            //TODO: CHECK TO SEE IF THE MESSAGE WHICH JUST CAME IN WAS A SECURITY KEY AND IF SO PARSE IT AND USE IT
            if (IncomingMessage.Message.Contains("Begin Key ---->"))
            {
                //GET THE KEY VIA REGEX
                EnabledMenu.Checked = true;
                string Key = Regex.Match(IncomingMessage.Message, "Begin Key ---->(?<Key>.*)<---- End Key", RegexOptions.IgnoreCase).Groups["Key"].Value;

                //ADD THE KEY TO THE LIST OF PUBLIC KEYS
                if (!PublicKeys.Keys.Contains(IncomingMessage.SenderID))
                    PublicKeys.Add(IncomingMessage.SenderID, Convert.FromBase64String(Key));

                //IF THIS MESSAGE CAME FROM US THEN JUST LET THE USER KNOW IT WORKED OTHERWISE TELL THE USER WE RECEIVED A KEY
                if (IncomingMessage.SenderID != this.Connection.UserID)
                {
                    this.EnabledMenu.Checked = true;
                    IncomingMessage.Message = string.Format("Public Key Received From {0}", IncomingMessage.Sender);
                    IncomingMessage.Sender = this.Name;
                }
                else
                {
                    //LET THE USER KNOW THE KEY WAS SUCCEFULLY PLACED IN THE CONVERSATION BECAUSE WE GOT OUR MESSAGE BACK
                    IncomingMessage.Message = "Key sent successfully!";
                    IncomingMessage.Sender = this.Name;
                }
            }
            else
            {
                if (EnabledMenu.Checked)
                {
                    try
                    {
                        EncryptedWrapper tmpMessage = EncryptedWrapper.Deserialize(Convert.FromBase64String(IncomingMessage.Message));
                        IncomingMessage.Message = EncryptedWrapper.DecryptMessage(tmpMessage, this.MyPrivateKey);
                    }
                    catch
                    {
                        //DO NOTHING BECAUSE IT MUST BE VALID
                    }
                }
            }
        }

        public class EncryptedWrapper
        {
            public byte[][] IVs;
            public byte[][] Keys;
            public byte[] Message;

            public static EncryptedWrapper EncryptMessage(string Message, Dictionary<string, byte[]> PublicKeys)
            {
                using (TripleDESCryptoServiceProvider tmpDes = new TripleDESCryptoServiceProvider())
                {
                    tmpDes.GenerateIV();
                    tmpDes.GenerateKey();
                    using (MemoryStream tmpStream = new MemoryStream())
                    {
                        using (CryptoStream cryptStream = new CryptoStream(tmpStream, tmpDes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cryptStream.Write(System.Text.ASCIIEncoding.ASCII.GetBytes(Message), 0, Message.Length);
                            cryptStream.Flush();
                            cryptStream.Close();

                            EncryptedWrapper tmpReturn = new EncryptedWrapper();
                            List<byte[]> encKeys = new List<byte[]>();
                            List<byte[]> encIVs = new List<byte[]>();

                            foreach (byte[] PubKey in PublicKeys.Values)
                            {
                                using (RSACryptoServiceProvider tmpRSA = new RSACryptoServiceProvider())
                                {
                                    tmpRSA.ImportCspBlob(PubKey);
                                    encKeys.Add(tmpRSA.Encrypt(tmpDes.Key, false));
                                    encIVs.Add(tmpRSA.Encrypt(tmpDes.IV, false));
                                }
                            }
                            tmpReturn.IVs = encIVs.ToArray();
                            tmpReturn.Keys = encKeys.ToArray();
                            tmpReturn.Message = tmpStream.ToArray();

                            return tmpReturn;
                        }
                    }
                }
            }

            public static string DecryptMessage(EncryptedWrapper Message, byte[] PrivateKey)
            {
                byte[] MessageKey = null;
                byte[] MessageIV = null;

                using (RSACryptoServiceProvider tmpRSA = new RSACryptoServiceProvider())
                {
                    tmpRSA.ImportCspBlob(PrivateKey);
                    foreach (byte[] tmpKey in Message.Keys)
                    {
                        try
                        {
                            MessageKey = tmpRSA.Decrypt(tmpKey, false);
                            break;
                        }
                        catch
                        {
                        }
                    }
                    foreach (byte[] tmpIV in Message.IVs)
                    {
                        try
                        {
                            MessageIV = tmpRSA.Decrypt(tmpIV, false);
                            break;
                        }
                        catch
                        {
                        }
                    }
                }

                using (MemoryStream tmpStream = new MemoryStream(Message.Message))
                {
                    using (TripleDESCryptoServiceProvider tmpDes = new TripleDESCryptoServiceProvider())
                    {
                        tmpDes.Key = MessageKey;
                        tmpDes.IV = MessageIV;

                        using (CryptoStream cryptStream = new CryptoStream(tmpStream, tmpDes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            int ReadLength;
                            byte[] buffer = new byte[1024];
                            StringBuilder tmpReturn = new StringBuilder();

                            while ((ReadLength = cryptStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                tmpReturn.Append(System.Text.ASCIIEncoding.ASCII.GetChars(buffer), 0, ReadLength);
                            }

                            return tmpReturn.ToString();
                        }
                    }
                }
            }

            public static byte[] Serialize(object Data)
            {
                //CREATE A SERIALIZER FOR THE TYPE PASSED IN
                XmlSerializer tmpSerializer = new XmlSerializer(typeof(EncryptedWrapper));

                //SERIALIZE THE OBJECT AND RETURN A NEW MESSAGE WRAPPER CONTAINING THE INFORMATION
                using (MemoryStream tmpStream = new MemoryStream())
                {
                    tmpSerializer.Serialize(tmpStream, Data);

                    return tmpStream.ToArray();
                }
            }

            public static EncryptedWrapper Deserialize(byte[] Data)
            {
                //CREATE A NEW SERIALIZER FOR THE TYPE PASSED IN
                XmlSerializer tmpSerializer = new XmlSerializer(typeof(EncryptedWrapper));

                //DE-SERIALIZE THE OBJECT AND RETURN IT TO THE CALLING FUNCTION
                using (MemoryStream tmpStream = new MemoryStream(Data))
                {
                    return (EncryptedWrapper)tmpSerializer.Deserialize(tmpStream);
                }
            }
        }

        public void Dispose()
        {
            this.PublicKeys.Clear();

            if (this.DebugWindow != null)
                this.DebugWindow.Close();

            this.ShowDebugMenu.Dispose();
            this.EnabledMenu.Dispose();
            this.MainMenu.Dispose();
        }
    }
}
