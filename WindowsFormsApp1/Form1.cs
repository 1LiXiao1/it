
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
    {
        public partial class Form1 : Form
        {
        private readonly string _connectionString = "Data Source=DESKTOP-8RU4O9V\\MSSQLFLUFFY; Initial Catalog=test_1; Integrated Security=True";

        public Form1()
            {
                InitializeComponent();
            }

            private void button1_Click(object sender, EventArgs e)
            {
                string username = textBox1.Text;
                string password = textBox2.Text;

                // Здесь нужно реализовать вашу проверку логина и пароля
                string role = CheckCredentials(username, password);

                if (role == "Администратор")
                {
                MessageBox.Show("Добро пожаловать Администратор", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form2 adminForm = new Form2();
                    adminForm.Show();
                }
                else if (role == "юзер")
                {
                MessageBox.Show("Добро пожаловать юзер", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form3 userForm = new Form3();
                    userForm.Show();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль. Попробуйте снова.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        private string CheckCredentials(string username, string password)
        {
            string role = "";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT yroven FROM log_1 WHERE login = @Username AND pass = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    role = reader["yroven"].ToString();
                }
                reader.Close();
            }
            return role;
        }

    }
} 
