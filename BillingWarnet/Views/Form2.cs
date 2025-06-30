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
using BillingWarnet.Controllers;

namespace BillingWarnet.Views
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DataTable dt = UserController.GetAllUsers();
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            string idUser = txtNama.Text.Trim();
            if (!int.TryParse(txtDurasi.Text.Trim(), out int durasi))
            {
                MessageBox.Show("Durasi harus berupa angka.");
                return;
            }

            if (string.IsNullOrEmpty(idUser))
            {
                MessageBox.Show("Nama user tidak boleh kosong.");
                return;
            }

            if (AuthController.IsUserExist(idUser))
            {
                MessageBox.Show("User sudah ada. Gunakan ID lain.");
                return;
            }

            if (UserController.AddUser(idUser, durasi))
            {
                MessageBox.Show("User berhasil ditambahkan dengan password default '123456'");
                LoadData();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Gagal menambahkan user.");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            string idUser = dataGridView1.CurrentRow.Cells["nama"].Value.ToString();
            if (!int.TryParse(txtDurasi.Text.Trim(), out int durasi))
            {
                MessageBox.Show("Durasi harus berupa angka.");
                return;
            }

            if (UserController.UpdateDurasi(idUser, durasi))
            {
                MessageBox.Show("Durasi user berhasil diupdate.");
                LoadData();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Gagal update durasi user.");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            string idUser = dataGridView1.CurrentRow.Cells["nama"].Value.ToString();

            DialogResult result = MessageBox.Show($"Yakin ingin menghapus user '{idUser}'?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;

            if (UserController.DeleteUser(idUser))
            {
                MessageBox.Show("User berhasil dihapus.");
                LoadData();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Gagal menghapus user.");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.OpenForms["Form1"]?.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtNama.Text = dataGridView1.Rows[e.RowIndex].Cells["nama"].Value.ToString();
                txtDurasi.Text = dataGridView1.Rows[e.RowIndex].Cells["durasi"].Value.ToString();
            }
        }

        private void ClearFields()
        {
            txtNama.Clear();
            txtDurasi.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Kosong (default event handler)
        }
    }
}