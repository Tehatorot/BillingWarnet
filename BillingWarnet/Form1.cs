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
        public Form1()
        {
            InitializeComponent();
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

            var result = DatabaseHelper.Login(id, pw);
            if (result != null)
            {
                var (role, durasi) = result.Value;

                DatabaseHelper.LogLogin(id);

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
                    Form3 formUser = new Form3(id, durasi);
                    formUser.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("ID atau Password salah.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormRegister regForm = new FormRegister();
            regForm.Show();
        }

        // Tambahan untuk hilangkan error designer
        private void txtID_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click_1(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
    }
}
