using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pract
{
    public partial class Search : Form
    {
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=FilmsBase1.mdb";
        private OleDbConnection myConn;
        public Search()
        {
            InitializeComponent();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            myConn = new OleDbConnection(connectString);
            myConn.Open();  
            string num=textBox1.Text;
            string query = "SELECT [SessionID],[MovieID],[DateTime],[Hall],[Price] FROM [Session] WHERE [Hall] LIKE '%" + num + "%'";
            OleDbDataAdapter comm = new OleDbDataAdapter(query,myConn);
            DataTable dt = new DataTable();
            comm.Fill(dt);
            dataGridView1.DataSource = dt;
            myConn.Close();
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }
    }
}
