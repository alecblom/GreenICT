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
    public partial class Form2 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Form2()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=D:\Study\Green ICT Application Project\Game Database.mdb;
Persist Security Info=False;";
        }
       
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = connection;
                cmd.CommandText = "insert into [Player]([username],[name],[password]) values('" + txt_username.Text + "','" + txt_name.Text + "','" + txt_pass.Text + "')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data saved");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
           
        }
    }
}
