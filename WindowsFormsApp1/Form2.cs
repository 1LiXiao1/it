using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "test_1DataSet.log_1". При необходимости она может быть перемещена или удалена.
            this.log_1TableAdapter.Fill(this.test_1DataSet.log_1);

        }
        private readonly string _connectionString = "Data Source=DESKTOP-8RU4O9V\\MSSQLFLUFFY; Initial Catalog=test_1; Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int nomer_3 = Convert.ToInt32(textBox1.Text); // Предполагается, что в txtAge вводится возраст как число.
                string login = textBox2.Text;
                string pass = textBox3.Text;
                string yroven = textBox4.Text;

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO log_1 (nomer_3, login, pass, yroven) VALUES (@nomer_3, @login, @pass, @yroven)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nomer_3", nomer_3);
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@pass", pass);
                        command.Parameters.AddWithValue("@yroven", yroven);
                        command.ExecuteNonQuery();
                    }
                }
                RefreshDataGridView();

                MessageBox.Show("Запись успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении записи: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefreshDataGridView()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM log_1";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "log_1");
                    dataGridView1.DataSource = dataSet.Tables["log_1"];
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idToDelete = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["login"].Value);

                string query = "DELETE FROM log_1 WHERE login = @id";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", idToDelete);
                        command.ExecuteNonQuery();
                    }
                }

                RefreshDataGridView();

                MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}