using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingWarnet
{
    public partial class Form3 : Form
    {
        private int sisaDetik;
        private string namaUser;

        public Form3(string nama, int durasiMenit)
        {
            InitializeComponent();
            namaUser = nama;
            sisaDetik = durasiMenit * 60; // Konversi dari menit ke detik
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            lblNama.Text = "Selamat datang, " + namaUser;
            UpdateLabelDurasi();

            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sisaDetik > 0)
            {
                sisaDetik--;
                UpdateLabelDurasi();
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Waktu habis. Terima kasih telah menggunakan layanan kami.");
                this.Close();
                Application.OpenForms["Form1"]?.Show();
            }
        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("Sesi dihentikan. Terima kasih.");
            this.Close();
            Application.OpenForms["Form1"]?.Show();
        }

        private void UpdateLabelDurasi()
        {
            TimeSpan ts = TimeSpan.FromSeconds(sisaDetik);
            lblDurasi.Text = "Sisa waktu: " + ts.ToString(@"hh\:mm\:ss");
        }
    }
}
