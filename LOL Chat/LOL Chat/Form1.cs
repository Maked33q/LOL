﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.SQLite;
using System.IO;
using System.Net.Sockets;
using System.Net;
using LOL_Chat.Classes;

namespace LOL_Chat
{
    public partial class FormMain : MaterialForm
    {
        private string db = "Chat_db.db";
        private string table1 = "Users";
        private string table2 = "Message";
        private string table3 = "KeyInfo";
        private SQLiteDataAdapter adapt;
        private DataTable dt;
        SQLiteConnection conn;
        FileSender fileSender = new FileSender();


        bool alive = false;
        UdpClient client;
        const int LOCALPORT = 8001;
        const int REMOTEPORT = 8001;
        const int TTL = 20;
        const string HOST = "235.5.5.1";
        IPAddress groupAddress;
        IPAddress remoteAddress;


        string userName;

        private static FormMain _instance;
        public static FormMain Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FormMain();
                    return _instance;
                }
                else
                {
                    return null;
                }
            }
        }

        public FormMain()
        {
            InitializeComponent();
            groupAddress = IPAddress.Parse(HOST);
            #region 
            if (!File.Exists(db))
            {
                SQLiteConnection.CreateFile(db);
            }
            conn = new SQLiteConnection($"Data Source={db}; Version=3;");
            conn.Open();
            string query = $"CREATE TABLE IF NOT EXISTS {table1} (" +
                "id INTEGER PRIMARY KEY NOT NULL, " +
                "name TEXT, " +
                "ip TEXT," +
                "isActive INTEGER )";

            SQLiteCommand cmd = new SQLiteCommand(query, conn);

            if (cmd.ExecuteNonQuery() == 0)
            {
                string[] query1 =
                {
            $"INSERT INTO {table1} (name,pass,isActive) VALUES('Jerry', '1.1.1.1',0)",
            $"INSERT INTO {table1} (name,pass,isActive) VALUES('Pedro', '1.1.1.1',0)",
            $"INSERT INTO {table1} (name,pass,isActive) VALUES('Tony', '1.1.1.1',0)",
            $"INSERT INTO {table1} (name,pass,isActive) VALUES('Bill', '1.1.1.1',0)",
            $"INSERT INTO {table1} (name,pass,isActive) VALUES('FFFF', '1.1.1.1',0)",
            $"INSERT INTO {table1} (name,pass,isActive) VALUES('Ron', '1.1.1.1',0)",
            $"INSERT INTO {table1} (name,pass,isActive) VALUES('Cot', '1.1.1.1',0)",
            $"INSERT INTO {table1} (name,pass,isActive) VALUES('Rot', '1.1.1.1',0)",
            $"INSERT INTO {table1} (name,pass,isActive) VALUES('Mot', '1.1.1.1',0)"
            };
                for (int i = 0; i < query1.Length; i++)
                {
                    cmd.CommandText = query1[i];
                    cmd.ExecuteNonQuery();
                }
            }

            query = $"CREATE TABLE IF NOT EXISTS {table2} (" +
                "public_key INTEGER, " +
                "key_blob INTEGER, " +
                "message TEXT," +
                "message_type TEXT )";
            cmd = new SQLiteCommand(query, conn);
            cmd.ExecuteNonQuery();


            query = $"CREATE TABLE IF NOT EXISTS {table3} (" +
                "key_id INTEGER PRIMARY KEY NOT NULL, " +
                "public_key INTEGER, " +
                "key_blob INTEGER)";
            cmd = new SQLiteCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            #endregion 
            #region
            MaterialSkinManager manager = MaterialSkinManager.Instance;
            manager.AddFormToManage(this);
            manager.Theme = MaterialSkinManager.Themes.DARK;
            manager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey700, Primary.Grey800, Accent.LightBlue200, TextShade.WHITE);
            #endregion

            userName = "default";
            try
            {
                client = new UdpClient(LOCALPORT);
                client.JoinMulticastGroup(groupAddress, TTL);
                Task receiveTask = new Task(ReceiveMessages);
                receiveTask.Start();
                string message = userName + " вошел в чат";
                byte[] data = Encoding.Unicode.GetBytes(message);
                client.Send(data, data.Length, HOST, REMOTEPORT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ReceiveMessages()
        {
            alive = true;
            try
            {
                while (alive)
                {
                    IPEndPoint remoteIp = null;
                    byte[] data = client.Receive(ref remoteIp);
                    string message = Encoding.Unicode.GetString(data);
                    if (String.IsNullOrEmpty(message))
                        return;
                    else
                        textBox1.Text = message + "\r\n" + textBox1.Text;

                }
            }
            catch (ObjectDisposedException)
            {
                if (!alive)
                    return;
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                string message = String.Format("{0}: {1}", userName, textBox2.Text);
                byte[] data = Encoding.Unicode.GetBytes(message);
                client.Send(data, data.Length, HOST, REMOTEPORT);
                textBox2.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void logoutButton_Click(object sender, EventArgs e)
        {
            ExitChat();
        }
        private void ExitChat()
        {
            string message = userName + " покидает чат";
            byte[] data = Encoding.Unicode.GetBytes(message);
            client.Send(data, data.Length, HOST, REMOTEPORT);
            client.DropMulticastGroup(groupAddress);
            alive = false;
            client.Close();
        }

        private void users_b_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt;*.docx)|*.txt;*.docx|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox1.Text = openFileDialog1.FileName;
                    Clipboard.SetText(textBox1.Text);
                    FileSender.Sender(Clipboard.GetText());
                    textBox1.Text = FileSender.SendFileInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


    }
}