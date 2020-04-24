using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Net;
namespace LOL_Chat
{
    public partial class Users : MaterialForm
    {
        //Base variable
        private string db = "Chat_db.db";
        private string table1 = "Users";
        private string table2 = "Groups";
        private SQLiteDataAdapter adapt;
        private DataTable dt;
        SQLiteConnection conn;
        //
        //Geting IP
        private string myIP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
        //
        private static string removedIP;


        public Users()
        {
            InitializeComponent();
            ////////////////////////////
            textBox1.Text = myIP;
            removedIP = textBox2.Text;
            ///////////////////////////
            try
            {
                conn = new SQLiteConnection($"Data Source={db};Version=3;");
                SQLiteCommand cmd = new SQLiteCommand($"Select * from {table1}", conn);
                SQLiteDataAdapter da = new SQLiteDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.DataSource = bsource;
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}