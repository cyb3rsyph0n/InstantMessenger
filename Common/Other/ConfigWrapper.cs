using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;
using Common.Messages;

namespace Common.Config
{
    public static class ConfigWrapper
    {
        public static string ConfigFilePath = Path.Combine(StaticFunctions.AppDataPath, "config.xml");
        public static string DefConfigFilePath = Path.Combine(Application.StartupPath, "config.xml");

        private static void CheckConfigExists()
        {
            //IF THE DEFAULT CONFIG FILE DOES NOT EXIST THEN CREATE IT
            if(!File.Exists(ConfigFilePath))
            {
                Directory.CreateDirectory(new FileInfo(ConfigFilePath).Directory.FullName);
                File.Copy(DefConfigFilePath, ConfigFilePath);
            }
        }

        public static List<Dictionary<string, string>> GetSetting(string SettingName)
        {
            //TODO: CHECK FOR A CONFIG FILE IN THE APP DATA PATH IF IT DOESNT EXIST THEN COPY FROM APP FOLDER SO THE USERS SETTINGS DO NOT CHANGE EVERY TIME A PUBLISH IS DONE
            CheckConfigExists();
            return GetSetting(ConfigFilePath, SettingName);
        }

        public static List<Dictionary<string, string>> GetSetting(string FileName, string SettingName)
        {
            XDocument xDoc = XDocument.Load(FileName);
            List<Dictionary<string, string>> tmpReturn = new List<Dictionary<string, string>>();

            //SELECT THE FIRST SETTINGS ELEMENT OR NULL
            var tmpElements = from a in xDoc.Descendants(SettingName) select a;

            //BUILD A RETURN ITEM WHICH CONTAINS EACH RETURNED ELEMENT FROM THE CONFIG FILE
            foreach (var tmpElement in tmpElements)
            {
                Dictionary<string, string> tmpDict = new Dictionary<string, string>();

                //LOOP THROUGH EACH ATTRIBUTE IT CONTAINS AND RETURN THE VALUE AS PART OF A LIST OF KEYVALUE PAIRS
                foreach (XAttribute tmpAtt in tmpElement.Attributes())
                {
                    tmpDict.Add(tmpAtt.Name.ToString(), tmpAtt.Value.ToString());
                }

                tmpReturn.Add(tmpDict);
            }
            //RETURN THE LIST OF KEYVALUE PAIRS WE CREATED
            return tmpReturn;
        }

        public static void SaveSetting(string SettingName, List<Dictionary<string, string>> Parameters)
        {
            SaveSetting(ConfigFilePath, SettingName, Parameters);
        }

        public static void SaveSetting(string FileName, string SettingName, List<Dictionary<string, string>> Parameters)
        {
            XDocument xDoc = XDocument.Load(FileName);
            var tmpNodes = from a in xDoc.Descendants(SettingName) select a;

            //IF WE DIDNT FIND THE NODE BY SELECTING ITS NAME THEN WE NEED TO CREATE ONE AND ADD IT TO THE SETTINGS AREA OF THE DOCUMENT
            while (tmpNodes.Count() != 0)
                tmpNodes.First().Remove();

            //REMOVE ALL THE ATTRIBUTES WHICH ARE ALREADY THERE BECAUSE WE ARE NOT DOING UPDATES WE ARE JUST SAVING WHAT THEY GAVE US
            //TODO: ADD THE ABILITY TO JUST UPDATE INSTEAD OF DELETE WOULDNT BE DIFFICULT
            foreach (Dictionary<string, string> tmpParam in Parameters)
            {
                XElement tmpElement = new XElement(SettingName);
                foreach (KeyValuePair<string, string> tmpPair in tmpParam)
                {
                    tmpElement.SetAttributeValue(tmpPair.Key, tmpPair.Value);
                }

                xDoc.Descendants("settings").First().Add(tmpElement);
            }

            //SAVE THE CHANGES WE MADE TO THE DOCUMENT
            xDoc.Save(FileName);
        }

        public static string EncryptString(string Input)
        {
            TripleDESCryptoServiceProvider tmpDes = new TripleDESCryptoServiceProvider();
            tmpDes.IV = new byte[] {64, 109, 216, 78, 95, 148, 253, 1};
            tmpDes.Key = new byte[] {24, 54, 98, 123, 253, 90, 198, 167, 86, 90, 45, 108, 56, 89, 98, 53, 188, 209, 200, 91, 10, 87, 77, 199};

            using (System.IO.MemoryStream tmpMem = new System.IO.MemoryStream())
            {
                using (CryptoStream tmpCryptStream = new CryptoStream(tmpMem, tmpDes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    tmpCryptStream.Write(System.Text.ASCIIEncoding.ASCII.GetBytes(Input), 0, Input.Length);
                    tmpCryptStream.Flush();
                    tmpCryptStream.Close();

                    return System.Convert.ToBase64String(tmpMem.ToArray());
                }
            }
        }

        public static string DecryptString(string Input)
        {
            TripleDESCryptoServiceProvider tmpDes = new TripleDESCryptoServiceProvider();
            StringBuilder tmpReturn = new StringBuilder();

            tmpDes.IV = new byte[] { 64, 109, 216, 78, 95, 148, 253, 1 };
            tmpDes.Key = new byte[] { 24, 54, 98, 123, 253, 90, 198, 167, 86, 90, 45, 108, 56, 89, 98, 53, 188, 209, 200, 91, 10, 87, 77, 199 };

            try
            {
                using (System.IO.MemoryStream tmpMem = new System.IO.MemoryStream(System.Convert.FromBase64String(Input)))
                {
                    using (CryptoStream tmpCryptStream = new CryptoStream(tmpMem, tmpDes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        int readCount = 0;
                        byte[] buffer = new byte[1024];

                        while ((readCount = tmpCryptStream.Read(buffer, 0, 1024)) != 0)
                        {
                            tmpReturn.Append(System.Text.ASCIIEncoding.ASCII.GetString(buffer, 0, readCount));
                        }

                        return tmpReturn.ToString();
                    }
                }
            }
            catch
            {
                return Input;
            }
        }
    }
}
