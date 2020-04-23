using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LOL_Chat.Classes
{
    public class FileSender
    {
        private static FileDetails fileDet = new FileDetails();

        private static IPAddress remonteIPAddress;
        private const int remontePort = 5002;
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
        static void Sender()
        {
            try
            {
                Console.WriteLine("Enter Remonte IP address");
                remonteIPAddress = IPAddress.Parse(Console.ReadLine().ToString());
                endPoint = new IPEndPoint(remonteIPAddress, remontePort);
                Console.WriteLine("Enter File path and name to send");
                fs = new FileStream(@Console.ReadLine().ToString(), FileMode.Open, FileAccess.Read);
                if (fs.Length > 8192)
                {
                    Console.Write("This version transfer files with size < 8192 bytes");
                    sender.Close();
                    fs.Close();
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public static void SendFileInfo()
        {
            fileDet.FILETYPE = fs.Name.Substring((int)fs.Name.Length - 3, 3);
            fileDet.FILESIZE = fs.Length;
            XmlSerializer fileSerializer = new XmlSerializer(typeof(FileDetails));
            MemoryStream stream = new MemoryStream();
            fileSerializer.Serialize(stream, fileDet);
            stream.Position = 0;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, Convert.ToInt32(stream.Length));

            Console.WriteLine("Sending file details...");
            sender.Send(bytes, bytes.Length, endPoint);
            stream.Close();
        }
        private static void SendFile()
        {
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            Console.WriteLine("Sending file...size=" + fs.Length + "bytes");
            try
            {
                sender.Send(bytes, bytes.Length, endPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                fs.Close();
                sender.Close();
            }
            Console.Read();
            Console.WriteLine("File sent successfully");
        }
    }
}
