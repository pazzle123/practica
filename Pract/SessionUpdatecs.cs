using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Pract
{
    public partial class SessionUpdatecs : Form
    {
        public Form1 MainForm { get; set; }
        public int SessionId { get; set; } // Добавляем свойство для хранения ID сеанса
        private static string connect = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=FilmsBase1.mdb";
        private OleDbConnection dbConnection;

        public SessionUpdatecs(int sessionId) // Добавляем параметр в конструктор
        {
            InitializeComponent();
            SessionId = sessionId;
            dbConnection = new OleDbConnection(connect);
            dbConnection.Open();
            LoadSessionData(); // Загружаем данные сеанса при открытии формы
        }

        private void LoadSessionData()
        {
            string query = "SELECT * FROM [Session] WHERE [SessionID] = ?";
            OleDbCommand command = new OleDbCommand(query, dbConnection);
            command.Parameters.AddWithValue("?", SessionId);

            using (OleDbDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    textBox1.Text = reader["MovieID"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["DateTime"]);
                    textBox2.Text = reader["Hall"].ToString();
                    textBox3.Text = reader["Price"].ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int movid = Convert.ToInt32(textBox1.Text);
                DateTime data = dateTimePicker1.Value;
                string hall = textBox2.Text;
                decimal price = Convert.ToDecimal(textBox3.Text); // Используем decimal для денег

                string query = @"UPDATE [Session] SET 
                               [MovieID] = ?, 
                               [DateTime] = ?, 
                               [Hall] = ?, 
                               [Price] = ? 
                               WHERE [SessionID] = ?";

                OleDbCommand command = new OleDbCommand(query, dbConnection);
                command.Parameters.AddWithValue("?", movid);
                command.Parameters.AddWithValue("?", data);
                command.Parameters.AddWithValue("?", hall);
                command.Parameters.AddWithValue("?", price);
                command.Parameters.AddWithValue("?", SessionId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Сеанс успешно обновлен!");
                    MainForm.RefreshData(); // Вызываем метод обновления данных в главной форме
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Сеанс не найден!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SessionUpdatecs_Load(object sender, EventArgs e)
        {
            // Код для загрузки формы (если нужен)
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Код для обработки нажатия кнопки 2
            // Например, закрытие формы:
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}