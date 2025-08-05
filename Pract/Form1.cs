using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

namespace Pract
{
    public partial class Form1 : Form
    {
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=FilmsBase1.mdb";
        private OleDbConnection myConn;

        public Form1()
        {
            InitializeComponent();
            myConn = new OleDbConnection(connectString);
            myConn.Open();
        }

        // Добавляем метод для обновления данных
        public void RefreshData()
        {
            this.sessionTableAdapter.Fill(this.filmsBase1DataSet.Session);
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void label2_Click(object sender, EventArgs e)
        {
            // Обработчик клика по label2 (если нужен)
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Обработчик клика по label3 (если нужен)
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Проверяем, введен ли ID сеанса
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Введите ID сеанса для редактирования!");
                textBox3.Focus();
                return;
            }

            // Проверяем, что введено число
            if (!int.TryParse(textBox3.Text, out int sessionId))
            {
                MessageBox.Show("ID сеанса должен быть целым числом!");
                textBox3.SelectAll();
                textBox3.Focus();
                return;
            }

            // Проверяем существование сеанса в базе данных
            try
            {
                string checkQuery = "SELECT COUNT(*) FROM [Session] WHERE [SessionID] = ?";

                using (OleDbCommand checkCmd = new OleDbCommand(checkQuery, myConn))
                {
                    checkCmd.Parameters.AddWithValue("?", sessionId);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists == 0)
                    {
                        MessageBox.Show($"Сеанс с ID {sessionId} не найден!");
                        textBox3.SelectAll();
                        textBox3.Focus();
                        return;
                    }
                }

                // Если сеанс существует, открываем форму редактирования
                SessionUpdatecs editForm = new SessionUpdatecs(sessionId);
                editForm.MainForm = this;
                editForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке сеанса: {ex.Message}");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
                string query = "DELETE FROM [Session] WHERE [SessionID] = ?";
                OleDbCommand command = new OleDbCommand(query, myConn);
                command.Parameters.AddWithValue("?", code);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Данные о сеансе удалены");
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SessionAdd ad = new SessionAdd();
            ad.Owner = this;
            ad.Show();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.sessionTableAdapter.Fill(this.filmsBase1DataSet.Session);
        }

        private void пToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сеансыПоЗалуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search sea = new Search();
            sea.Owner = this;
            sea.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void LoadData(string sortOrder = "")
        {
            using (OleDbConnection connection = new OleDbConnection(connectString))
            {
                string query = "SELECT [SessionID], [MovieID], [DateTime],[Hall],[Price] FROM [Session]";

                if (!string.IsNullOrEmpty(sortOrder))
                {
                    query += $" ORDER BY Price {sortOrder}";
                }

                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView1.DataSource = table;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            LoadData("ASC");
        }
        

        private void button7_Click_1(object sender, EventArgs e)
        {
            LoadData("DESC");
        }
    }
}