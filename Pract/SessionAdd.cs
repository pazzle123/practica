using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pract
{
    public partial class SessionAdd : Form
    {
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=FilmsBase1.mdb";
        private OleDbConnection myConn;

        public SessionAdd()
        {
            InitializeComponent();
            myConn = new OleDbConnection(connectString);
            myConn.Open();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sesid = Convert.ToInt32(textBox4.Text);
            int movid = Convert.ToInt32(textBox1.Text);
            DateTime data = dateTimePicker1.Value;
            string hall = textBox2.Text;
            decimal pric = Convert.ToDecimal(textBox3.Text);
            string query = "INSERT INTO [Session] ([SessionID],[MovieID],[DateTime],[Hall],[Price]) VALUES (" + sesid + ", " + movid + ", '" + data.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + hall + "', " + pric.ToString(CultureInfo.InvariantCulture) + ")";
            OleDbCommand command = new OleDbCommand(query,myConn);
            command.ExecuteNonQuery();
            MessageBox.Show("Данные о сеансе добавлены");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SessionAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
