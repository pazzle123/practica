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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pract
{
    public partial class FilmUpdate : Form
    {
        public Form2 MainForm { get; set; }
        public int MovieId { get; set; } // Добавляем свойство для хранения ID сеанса
        private static string connect = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=FilmsBase1.mdb";
        private OleDbConnection dbConnection;
        public FilmUpdate(int movieId)
        {
            InitializeComponent();
            MovieId = movieId;
            dbConnection = new OleDbConnection(connect);
            dbConnection.Open();
            LoadFilmData();
        }
        private void LoadFilmData()
        {
            string query = "SELECT * FROM [Movie] WHERE [MovieID] = ?";
            OleDbCommand command = new OleDbCommand(query, dbConnection);
            command.Parameters.AddWithValue("?", MovieId);

            using (OleDbDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    textBox4.Text = reader["Title"].ToString();
                   
                    textBox2.Text = reader["Genre"].ToString();
                    textBox3.Text = reader["Duration"].ToString();
                    textBox5.Text = reader["Rating"].ToString();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string title = textBox4.Text;
                string genre = textBox2.Text;
                string duration = textBox3.Text;
                string rating = textBox5.Text;

                string query = @"UPDATE [Movie] SET 
                   [Title] = ?, 
                   [Genre] = ?, 
                   [Duration] = ?, 
                   [Rating] = ? 
                   WHERE [MovieID] = ?";

                OleDbCommand command = new OleDbCommand(query, dbConnection);
                command.Parameters.AddWithValue("?", title);
                command.Parameters.AddWithValue("?", genre);
                command.Parameters.AddWithValue("?", duration);
                command.Parameters.AddWithValue("?", rating);
                command.Parameters.AddWithValue("?", MovieId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Данные фильма успешно обновлены!");
                    MainForm.RefreshData(); // Вызываем метод обновления данных в главной форме
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Фильм не найден!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
