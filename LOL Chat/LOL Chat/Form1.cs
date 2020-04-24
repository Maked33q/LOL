using System;
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

            ///////////////////////////////////////////////////////////////////////
            MaterialSkinManager manager = MaterialSkinManager.Instance;
            manager.AddFormToManage(this);
            manager.Theme = MaterialSkinManager.Themes.DARK;
            manager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey700, Primary.Grey800, Accent.LightBlue200, TextShade.WHITE);
            ///////////////////////////////////////////////////////////////////////
        }

        private void users_b_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
        }
    }
}
