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
    public partial class SearchFilm : Form
    {
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=FilmsBase1.mdb";
        private OleDbConnection myConn;
        public SearchFilm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            myConn = new OleDbConnection(connectString);
            myConn.Open();
            string genre = textBox1.Text;
            string query = "SELECT [MovieID],[Title],[Genre],[Duration],[Rating] FROM [Movie] WHERE [Genre] LIKE '%" + genre + "%'";
            OleDbDataAdapter comm = new OleDbDataAdapter(query, myConn);
            DataTable dt = new DataTable();
            comm.Fill(dt);
            dataGridView1.DataSource = dt;
            myConn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
