**Brando Cerah NET**

Brando Cerah NET adalah sebuah aplikasi billing warnet berbasis desktop yang dibangun menggunakan C# dan Windows Forms, serta terintegrasi dengan MySQL (XAMPP) sebagai basis datanya. Aplikasi ini memiliki dua jenis pengguna: admin dan user, masing-masing dengan antarmuka (Form) dan fungsi yang berbeda.



🎯 Fitur Utama

👨‍💼 Admin (Form2)
- Login dengan autentikasi sebagai admin.
- Menambahkan akun user melalui form registrasi.
- Melihat dan mengelola data pelanggan (fitur manajemen lanjutan bisa dikembangkan).
- Antarmuka sederhana untuk pengelolaan akun warnet.


👨‍💻 User (Form3)
- Login dengan ID dan password yang telah didaftarkan.
- Setelah login, waktu sesi akan berjalan sesuai dengan durasi yang ditentukan admin.
- Timer hitung mundur (countdown) secara real-time ditampilkan.
- Saat waktu habis, sesi akan ditutup otomatis.
- Tombol Selesai untuk mengakhiri sesi lebih awal.



🧱 Teknologi yang Digunakan

- Bahasa Pemrograman: C# (.NET Framework)
- UI Framework: Windows Forms (WinForms)
- Database: MySQL (melalui XAMPP)
- ORM: MySql.Data (library koneksi database MySQL untuk C#)


🛠️ Cara Menjalankan

1. Setup Database
   - Jalankan XAMPP dan aktifkan MySQL.
   - Buat database baru bernama `warnet_db`.
   - Buat tabel `users` dengan struktur:
     ```sql
     CREATE TABLE users (
         id_user VARCHAR(50) PRIMARY KEY,
         password VARCHAR(50),
         role ENUM('admin', 'user'),
         durasi INT
     );
     ```

   - Masukkan data admin dan user:
     ```sql
     INSERT INTO users (id_user, password, role, durasi)
     VALUES ('Brando', '980', 'admin', 0),
            ('User1', '123', 'user', 60);
     ```

2. Buka Project di Visual Studio
   - Pastikan sudah mengatur reference `MySql.Data`.
   - Buka `Form1.cs`, `Form2.cs`, dan `Form3.cs` untuk melihat atau menyesuaikan logika.
   - Jalankan program dengan `Start` (F5).



📌 Catatan

- Kolom `durasi` pada user berfungsi untuk menentukan berapa lama user dapat menggunakan komputer setelah login (dalam satuan menit).
- Waktu akan ditampilkan dalam format `HH:MM:SS` dan dihitung mundur setiap detik.
- Jika waktu habis, user akan otomatis logout dan kembali ke form login.
