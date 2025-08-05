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
    public partial class Form2 : Form
    {
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=FilmsBase1.mdb";
        private OleDbConnection myConn;
        private object sessionTableAdapter;

        public Form2()
        {
            InitializeComponent();
            myConn = new OleDbConnection(connectString);
            myConn.Open();
        }
        public void RefreshData()
        {
            this.movieTableAdapter.Fill(this.filmsBase1DataSet.Movie);

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "filmsBase1DataSet.Movie". При необходимости она может быть перемещена или удалена.
            this.movieTableAdapter.Fill(this.filmsBase1DataSet.Movie);

        }
        


        private void Form2_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            myConn.Close();
            Main mainForm = Application.OpenForms.OfType<Main>().FirstOrDefault();

            if (mainForm != null)
            {
                mainForm.Show(); // Показываем Main, если он был скрыт
            }
            else
            {
                // Если Main не был открыт, создаём новый экземпляр
                Main newMain = new Main();
                newMain.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int code = Convert.ToInt32(textBox2.Text);
                string query = "DELETE FROM [Movie] WHERE [MovieID] = ?";
                OleDbCommand command = new OleDbCommand(query, myConn);
                command.Parameters.AddWithValue("?", code);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Данные о фильме удалены");
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Сеанс с указанным ID не найден");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Введите ID сеанса для редактирования!");
                textBox3.Focus();
                return;
            }

            // Проверяем, что введено число
            if (!int.TryParse(textBox3.Text, out int movieId))
            {
                MessageBox.Show("ID фильмв должен быть целым числом!");
                textBox3.SelectAll();
                textBox3.Focus();
                return;
            }

            // Проверяем существование сеанса в базе данных
            try
            {
                string checkQuery = "SELECT COUNT(*) FROM [Movie] WHERE [MovieID] = ?";

                using (OleDbCommand checkCmd = new OleDbCommand(checkQuery, myConn))
                {
                    checkCmd.Parameters.AddWithValue("?", movieId);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists == 0)
                    {
                        MessageBox.Show($"Фильм с ID {movieId} не найден!");
                        textBox3.SelectAll();
                        textBox3.Focus();
                        return;
                    }
                }

                // Если сеанс существует, открываем форму редактирования
                FilmUpdate editForm = new FilmUpdate(movieId);
                editForm.MainForm = this;
                editForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке фильмы: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FilmAdd ad = new FilmAdd();
            ad.Owner = this;
            ad.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.movieTableAdapter.Fill(this.filmsBase1DataSet.Movie);
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сеансыПоЗалуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchFilm sea = new SearchFilm();
            sea.Owner = this;
            sea.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            myConn.Close();
            Main mainForm = Application.OpenForms.OfType<Main>().FirstOrDefault();

            if (mainForm != null)
            {
                mainForm.Show();
                this.Close();// Показываем Main, если он был скрыт
            }
            else
            {
                // Если Main не был открыт, создаём новый экземпляр
                Main newMain = new Main();
                newMain.Show();
                this.Close();
            }
        }
        private void LoadData(string sortOrder = "")
        {
            using (OleDbConnection connection = new OleDbConnection(connectString))
            {
                string query = "SELECT [MovieID], [Title],[Genre],[Duration],[Rating] FROM [Movie]";

                if (!string.IsNullOrEmpty(sortOrder))
                {
                    query += $" ORDER BY [Rating] {sortOrder}";
                }

                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView1.DataSource = table;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadData("ASC");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadData("DESC");
        }
    }
}
