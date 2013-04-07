using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Common.Connections
{
    public class Connection
    {
        public TcpClient TcpConnection;
        public NetworkStream NetStream;
        public string UserID;
        public string DisplayName;
        public bool Authenticated = false;
        private Mutex key = new Mutex(false);

        public Connection()
        {
            this.NetStream = null;
            this.TcpConnection = null;
            this.UserID = "";
        }

        public static bool operator == (Connection a, Connection b)
        {
            if(a.TcpConnection != b.TcpConnection || a.UserID != b.UserID || a.NetStream != b.NetStream)
                return false;
            else
                return true;
        }

        public static bool operator !=(Connection a, Connection b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return this == (Connection)obj;
        }

        public override int GetHashCode()
        {
            return UserID.GetHashCode();
        }

        public void SafeWrite(byte[] Data)
        {
            //SETUP THE HEADER AND THE FOOTER SO WE CAN APPEND IT TO THE PACKET GOING OUT
            byte[] Header = System.Text.ASCIIEncoding.ASCII.GetBytes("PACKET_HEADER");
            byte[] Footer = System.Text.ASCIIEncoding.ASCII.GetBytes("PACKET_FOOTER"); 
            
            //MAKE SURE WE LOCK AND WAIT ON A MUTEX BEFORE WE WRITE (THIS KEEPS PACKETS FROM GETTING DATA CORRUPTED
            this.key.WaitOne(Timeout.Infinite);
            
            //IF THE SEND BUFFER LENGTH IS NOT BIG ENOUGH THEN MAKE IT BIG ENOUGH TO SUPPORT THE DATA WE ARE SENDING
            if (Header.Length + Footer.Length + Data.Length > this.TcpConnection.SendBufferSize)
                this.TcpConnection.SendBufferSize = Header.Length + Footer.Length + Data.Length;

            try
            {
                //WRITE THE HEADER, DATA, THEN FOOTER
                this.NetStream.Write(Header, 0, Header.Length);
                this.NetStream.Write(Data, 0, Data.Length);
                this.NetStream.Write(Footer, 0, Footer.Length);
            }
            catch
            {
            }

            //RELEASE THE MUTEX SO OTHER THREADS CAN USE THIS TO WRITE
            this.key.ReleaseMutex();
        }
    }
}