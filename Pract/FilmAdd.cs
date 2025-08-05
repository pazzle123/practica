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
    public partial class FilmAdd : Form
    {
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=FilmsBase1.mdb";
        private OleDbConnection myConn;
        public FilmAdd()
        {
            InitializeComponent();
            myConn = new OleDbConnection(connectString);
            myConn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int movid = Convert.ToInt32(textBox1.Text);
            
            string title = textBox3.Text;
            string genre = textBox2.Text;
            string prod = textBox4.Text;
            string rating = textBox5.Text;
            string query = "INSERT INTO [Movie] ([MovieID], [Title], [Genre], [Duration], [Rating]) " +
                  "VALUES (@MovieID, @Title, @Genre, @Duration, @Rating)";

            // Создаем команду с параметрами
            using (OleDbCommand command = new OleDbCommand(query, myConn))
            {
                // Добавляем параметры с соответствующими значениями
                command.Parameters.AddWithValue("@MovieID", movid);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Genre", genre);
                command.Parameters.AddWithValue("@Duration", prod);
                command.Parameters.AddWithValue("@Rating", rating);

                // Выполняем запрос
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Данные о фильме успешно добавлены!");
                }
                else
                {
                    MessageBox.Show("Не удалось добавить данные о фильме");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void FilmAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
