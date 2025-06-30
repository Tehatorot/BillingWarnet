using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BillingWarnet.Controllers;

namespace BillingWarnet.Views
{
    public partial class Form3 : Form
    {
        private string namaUser;
        private string idUser;
        private DateTime waktuBerakhir;

        public Form3(string id, int durasiMenit)
        {
            InitializeComponent();
            idUser = id;
            namaUser = id;
            waktuBerakhir = DateTime.Now.AddMinutes(durasiMenit);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            lblNama.Text = "Selamat datang, " + namaUser;

            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan sisa = waktuBerakhir - DateTime.Now;

            if (sisa.TotalSeconds > 0)
            {
                lblDurasi.Text = "Sisa waktu: " + sisa.ToString(@"hh\:mm\:ss");
            }
            else
            {
                timer1.Stop();
                lblDurasi.Text = "Sisa waktu: 00:00:00";
                MessageBox.Show("Waktu habis. Terima kasih telah menggunakan layanan kami.");

                UserController.UpdateDurasiLangsung(idUser, 0);

                this.Close();
                Application.OpenForms["Form1"]?.Show();
            }
        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            TimeSpan sisa = waktuBerakhir - DateTime.Now;

            int sisaMenit = Math.Max((int)sisa.TotalMinutes, 0);

            UserController.UpdateDurasiLangsung(idUser, sisaMenit);

            MessageBox.Show("Sesi dihentikan. Terima kasih.");
            this.Close();
            Application.OpenForms["Form1"]?.Show();
        }

        private void lblNama_Click(object sender, EventArgs e) { }
    }
}