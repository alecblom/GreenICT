using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication11
{
    public partial class Form1 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Form1()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=D:\Study\Green ICT Application Project\Game Database.mdb;
Persist Security Info=False;";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select * from Player where username='"+ textBox1.Text+ "' and password='"+textBox2.Text+"'";
            OleDbDataReader reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++;
            }
            if (count == 1)
            {
                MessageBox.Show("Username and password is correct");
                connection.Close();
                connection.Dispose();
                this.Hide();
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }
            if (count > 1)
            {
                MessageBox.Show("Duplicate Username and password");
            }
            if (count < 1)
            {
                MessageBox.Show("Username and password is not correct");
            }
            connection.Close();
        }
    }
}
