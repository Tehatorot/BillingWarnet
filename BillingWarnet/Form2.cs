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
    public partial class Form2 : Form
    {
        string connStr = "server=localhost;user=root;password=;database=warnet_db;";

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
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM pelanggan";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal load data: " + ex.Message);
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            string nama = txtNama.Text.Trim();
            if (!int.TryParse(txtDurasi.Text.Trim(), out int durasi))
            {
                MessageBox.Show("Durasi harus berupa angka.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO pelanggan (nama, durasi) VALUES (@nama, @durasi)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nama", nama);
                    cmd.Parameters.AddWithValue("@durasi", durasi);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data berhasil ditambahkan.");
                    LoadData();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal tambah data: " + ex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
            string nama = txtNama.Text.Trim();
            if (!int.TryParse(txtDurasi.Text.Trim(), out int durasi))
            {
                MessageBox.Show("Durasi harus berupa angka.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE pelanggan SET nama=@nama, durasi=@durasi WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nama", nama);
                    cmd.Parameters.AddWithValue("@durasi", durasi);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data berhasil diupdate.");
                    LoadData();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal update data: " + ex.Message);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);

            DialogResult result = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM pelanggan WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data berhasil dihapus.");
                    LoadData();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal hapus data: " + ex.Message);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close(); // Tutup Form2
            Application.OpenForms["Form1"]?.Show(); // Tampilkan kembali Form1
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
    }
}