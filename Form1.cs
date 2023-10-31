using System.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace TPfinalProgramacion1
{
    public partial class Form1 : Form
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=DATABASE85;Integrated Security=True;";
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            string nombre, apellido, carrera;
            int dni;
            dni = Convert.ToInt32(textBox1.Text);
            nombre = textBox2.Text;
            apellido = textBox3.Text;
            carrera = comboBox1.Text;
            DateTime fechanacimiento;
            DateTime fechalimite = new DateTime(2005, 12, 31);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abre la conexi�n a la base de datos.
                connection.Open();

                // Verifica si el DNI ya existe en la tabla de estudiantes.
                string query = "SELECT COUNT(*) FROM estudiantes WHERE DNI = @dni";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@dni", dni);
                    int count = (int)command.ExecuteScalar();


                    if (count > 0 || !int.TryParse(textBox1.Text, out dni) || dni <= 0 ||
                   string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) ||
                   string.IsNullOrWhiteSpace(carrera) || comboBox1.SelectedIndex < 0 ||
                   !DateTime.TryParse(textBox4.Text, out fechanacimiento) || fechanacimiento > fechalimite)
                    {
                        MessageBox.Show("Por favor, complete todos los campos correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // Realiza las acciones necesarias con las variables
            // ...
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "INSERT INTO Estudiantes (dni, nombre, apellido, fechanacimiento, carrera) VALUES (@dni, @nombre, @apellido, @fechanacimiento, @carrera)";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@dni", dni);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@apellido", apellido);
                    command.Parameters.AddWithValue("@fechanacimiento", fechanacimiento);
                    command.Parameters.AddWithValue("@carrera", carrera);

                    command.ExecuteNonQuery();

                }
            }

            MessageBox.Show("Datos guardados correctamente.");
            button3.Visible = true;
            textBox4.Visible = false;
            textBox3.Clear();
            textBox2.Clear();
            textBox1.Clear();
            comboBox1.ResetText();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 FormularioCarga = new Form2();
            FormularioCarga.Show();
            FormularioCarga.CargarDatos();
        }

        private void btnAbrirFormularioLogin_Click(object sender, EventArgs e)
        {
            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Las credenciales son v�lidas, habilita el otro bot�n
                    button2.Enabled = true;
                    MessageBox.Show("Inicio de sesi�n exitoso");
                    btnAbrirFormularioLogin.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Inicio de sesi�n fallido");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem as string;

            // Calcular el ancho m�ximo de los elementos
            int maxWidth = 0;
            foreach (var item in comboBox1.SelectedItem as string)
            {
                int itemWidth = TextRenderer.MeasureText(selectedValue.ToString(), comboBox1.Font).Width;
                if (itemWidth > maxWidth)
                {
                    maxWidth = itemWidth;
                }
            }

            // Establecer el ancho de la ComboBox al ancho m�ximo encontrado
            comboBox1.Width = maxWidth + 18;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            textBox4.Visible = true;
            monthCalendar1.Visible = true;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionStart;
            textBox4.Text = selectedDate.ToShortDateString();

            monthCalendar1.Visible = false;
        }
    }
}