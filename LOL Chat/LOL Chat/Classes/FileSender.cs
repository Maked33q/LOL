using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LOL_Chat.Classes
{
    public class FileSender
    {
        private static FileDetails fileDet = new FileDetails();
        const string HOST = "235.5.5.1";
        private static IPAddress remonteIPAddress = IPAddress.Parse(HOST);
        private const int remontePort = 8001;
        private static UdpClient sender = new UdpClient();
        private static IPEndPoint endPoint;

        private static FileStream fs;

        [Serializable]
        public class FileDetails
        {
            public string FILETYPE = "";
            public long FILESIZE = 0;
        }
        [STAThread]
        public static void Sender(string filePath)
        {
            try
            {
                endPoint = new IPEndPoint(remonteIPAddress, remontePort);
                Console.WriteLine("Enter File path and name to send");
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                if (fs.Length > 8192)
                {
                    MessageBox.Show("File is too big!");
                    sender.Close();
                    fs.Close();
                    SendFile();
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public static string SendFileInfo()
        {
            fileDet.FILETYPE = fs.Name.Substring((int)fs.Name.Length - 3, 3);
            fileDet.FILESIZE = fs.Length;
            XmlSerializer fileSerializer = new XmlSerializer(typeof(FileDetails));
            MemoryStream stream = new MemoryStream();
            fileSerializer.Serialize(stream, fileDet);
            stream.Position = 0;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, Convert.ToInt32(stream.Length));
            string str; 
            sender.Send(bytes, bytes.Length, endPoint);
            stream.Close();
            return str = "Sending file details...";
        }
        private static string SendFile()
        {
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            //Console.WriteLine("Sending file...size=" + fs.Length + "bytes");
            try
            {
                sender.Send(bytes, bytes.Length, endPoint);
            }
            catch (Exception e)
            {
               MessageBox.Show(e.ToString());
            }
            finally
            {
                fs.Close();
                sender.Close();
            }
            string str;
            return str = "File sent successfully";
        }
    }
}
