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

            if (!AuthController.Register(id, pw))
            {
                MessageBox.Show("ID sudah digunakan atau registrasi gagal.");
                return;
            }

            MessageBox.Show("Registrasi berhasil! Silakan login.");
            this.Close();

        }
    }
}
