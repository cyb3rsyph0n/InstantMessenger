using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.IO.Compression;

namespace Common.Messages
{
    public class MessageWrapper
    {
        //HOLDS THE CONTENTS OF THE MESSAGE SO IT CAN BE UNWRAPPED ON THE OTHER SIDE
        public string MessageType { get; set; }
        public byte[] Data { get; set; }

        //USED TO MAKE EACH MESSAGE UNIQUE EVEN IF THE REST OF THE CONTENTS ARE THE SAME
        public long TimeStamp = DateTime.Now.Ticks;

        public MessageWrapper()
        {
            this.MessageType = null;
            this.Data = null;
        }

        public MessageWrapper(Type MessageType, byte[] Data)
        {
            this.MessageType = MessageType.ToString();
            this.Data = Data;
        }

        private static MessageWrapper WrapMessage(Type ObjType, object Data)
        {
            //CREATE A SERIALIZER FOR THE TYPE PASSED IN
            XmlSerializer tmpSerializer = new XmlSerializer(ObjType);

            //SERIALIZE THE OBJECT AND RETURN A NEW MESSAGE WRAPPER CONTAINING THE INFORMATION
            using (MemoryStream tmpStream = new MemoryStream())
            {
                tmpSerializer.Serialize(tmpStream, Data);

                return new MessageWrapper(ObjType, tmpStream.ToArray());
            }
        }
        
        private static Object UnWrapMessage(Type ObjType, byte[] Data)
        {
            //CREATE A NEW SERIALIZER FOR THE TYPE PASSED IN
            XmlSerializer tmpSerializer = new XmlSerializer(ObjType);

            //DE-SERIALIZE THE OBJECT AND RETURN IT TO THE CALLING FUNCTION
            using (MemoryStream tmpStream = new MemoryStream(Data))
            {
                return tmpSerializer.Deserialize(tmpStream);
            }
        }

        private static byte[] PackOutterMessage(MessageWrapper SendingMessage)
        {
            //PACK THE MESSAGE INTO A MESSAGE WRAPPER WITH A KNOWN TYPE
            return WrapMessage(SendingMessage.GetType(), SendingMessage).Data;
        }

        private static MessageWrapper UnPackOutterMessage(byte[] Data)
        {
            //THIS IS USED ONLY TO UNWRAP THE EXTERNAL MESSAGE
            return (MessageWrapper)UnWrapMessage(typeof(MessageWrapper), Data);
        }
     
        private static byte[] CompressBytes(byte[] Data)
        {
            using (MemoryStream tmpMemStream = new MemoryStream())
            {
                using (GZipStream tmpStream = new GZipStream(tmpMemStream, System.IO.Compression.CompressionMode.Compress, false))
                {
                    //WRITE THE DATA TO THE NEWLY CREATED COMPRESSED STREAM
                    tmpStream.Write(Data, 0, Data.Length);
                    tmpStream.Flush();
                    tmpStream.Close();

                    //RETURN THE MEMORY ARRAY AS A BYTE ARRAY
                    return tmpMemStream.ToArray();
                }
            }
        }
   
        private static byte[] DecompressBytes(byte[] Data)
        {
            List<byte> tmpReturn = new List<byte>();

            using (MemoryStream tmpMemStream = new MemoryStream(Data))
            {
                using (GZipStream tmpStream = new GZipStream(tmpMemStream, System.IO.Compression.CompressionMode.Decompress))
                {
                    byte[] tmpBuffer = new byte[1024];
                    int readLength;

                    //WHILE THERE IS STILL DATA ON THE STREAM KEEP READING
                    while ((readLength = tmpStream.Read(tmpBuffer, 0, tmpBuffer.Length)) != 0)
                    {
                        tmpReturn.AddRange(tmpBuffer.Take(readLength));
                    }

                    //RETURN THE MEMORYSTREAM AS A BYTE ARRAY
                    return tmpReturn.ToArray();
                }
            }
        }

        public static byte[] PackageForTCP(Type ObjType, object Data)
        {
            //CREATE A NEW CONTAINER FOR SENDING THE MESSAGE
            MessageWrapper tmpWrapper = WrapMessage(ObjType, Data);
            
            //COMPRESS THE DATA FOR SENDING ACROSS THE STREAM
            tmpWrapper.Data = CompressBytes(tmpWrapper.Data);

            //RETURN THE NEWLY CREATED PACKAGE AFTER IT ALSO HAS BEEN SERIALIZED
            return PackOutterMessage(tmpWrapper);
        }

        public static object UnPackageFromTCP(byte[] Data, bool InnerPackage)
        {
            //CHECK TO SEE IF WE ARE SUPPOSED TO UNWRAP THE INNER PAKAGE OR BOTH
            if (!InnerPackage)
                return (object)UnPackOutterMessage(Data);
            else
            {
                //UNWRAP THE OUTTER PACKAGE
                MessageWrapper tmpWrapper = (MessageWrapper)UnPackOutterMessage(Data);

                //RETURN THE UNWRAPPED INNER PACKAGE
                return UnWrapMessage(StaticFunctions.GetObjType(tmpWrapper.MessageType), DecompressBytes(tmpWrapper.Data));
            }
        }

        private static byte[] EncryptBytes(byte[] Data)
        {
            //TODO CREATE AN ENCRYPTION ALGORITHM
            return null;
        }

        private static byte[] DecryptBytes(byte[] Data)
        {
            //TODO CREATE AN DECRYPTION ALGORITHM
            return null;
        }
    }
}
