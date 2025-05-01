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
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim();
            string pw = txtPassword.Text.Trim();

            if (id == "" || pw == "")
            {
                MessageBox.Show("ID dan Password tidak boleh kosong.");
                return;
            }

            string connStr = "server=localhost;port=3306;user=root;password=;database=warnet_db;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // Cek apakah ID sudah digunakan
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE id_user = @id";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@id", id);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("ID sudah digunakan. Gunakan ID lain.");
                        return;
                    }

                    string query = "INSERT INTO users (id_user, password, role) VALUES (@id, @pw, 'user')";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@pw", pw);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registrasi berhasil! Silakan login.");
                    this.Close(); // Tutup form register
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
        }
    }
}