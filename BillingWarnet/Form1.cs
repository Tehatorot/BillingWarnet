using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BillingWarnet
{
    public partial class Form1 : Form
    {
        // Koneksi database XAMPP (MySQL)
        string connStr = "server=localhost;port=3306;user=root;password=;database=warnet_db;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim();
            string pw = txtPassword.Text.Trim();

            if (id == "" || pw == "")
            {
                MessageBox.Show("Silakan isi ID dan Password terlebih dahulu.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT durasi, role FROM users WHERE id_user = @id AND password = @pw";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@pw", pw);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader.GetString("role");
                            int durasi = reader.GetInt32("durasi");

                            if (role == "admin")
                            {
                                MessageBox.Show("Login admin berhasil!");
                                Form2 formAdmin = new Form2();
                                formAdmin.Show();
                                this.Hide();
                            }
                            else if (role == "user")
                            {
                                MessageBox.Show("Login user berhasil!");
                                Form3 formUser = new Form3(id, durasi); // kirim nama & durasi
                                formUser.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("ID atau Password salah.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Koneksi ke database gagal:\n" + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormRegister regForm = new FormRegister();
            regForm.Show();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
