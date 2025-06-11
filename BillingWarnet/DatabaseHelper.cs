using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace BillingWarnet
{
    public static class DatabaseHelper
    {
        private static string connStr = "server=localhost;port=3306;user=root;password=;database=warnet_db;";

        public static MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi ke database gagal: " + ex.Message);
                return null;
            }
        }

        public static int ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                if (conn == null) return 0;

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string query, params MySqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                if (conn == null) return null;

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static MySqlDataReader ExecuteReader(string query, params MySqlParameter[] parameters)
        {
            var conn = GetConnection();
            if (conn == null) return null;

            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddRange(parameters);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static DataTable GetDataTable(string query, params MySqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                if (conn == null) return null;

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // LOGIN
        public static (string role, int durasi)? Login(string id, string pw)
        {
            string query = "SELECT durasi, role FROM users WHERE id_user = @id AND password = @pw";
            MySqlParameter[] parameters = {
                new MySqlParameter("@id", id),
                new MySqlParameter("@pw", pw)
            };

            using (var reader = ExecuteReader(query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    string role = reader.GetString("role");
                    int durasi = reader.GetInt32("durasi");
                    return (role, durasi);
                }
            }
            return null;
        }

        public static void LogLogin(string id)
        {
            string query = "INSERT INTO login_logs (id_user, login_time) VALUES (@id, NOW())";
            MySqlParameter[] parameters = {
                new MySqlParameter("@id", id)
            };
            ExecuteNonQuery(query, parameters);
        }

        // REGISTRASI
        public static bool IsUserExist(string id)
        {
            string query = "SELECT COUNT(*) FROM users WHERE id_user = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", id) };
            object result = ExecuteScalar(query, parameters);
            return Convert.ToInt32(result) > 0;
        }

        public static bool RegisterUser(string id, string pw)
        {
            string query = "INSERT INTO users (id_user, password, role) VALUES (@id, @pw, 'user')";
            MySqlParameter[] parameters = {
                new MySqlParameter("@id", id),
                new MySqlParameter("@pw", pw)
            };
            return ExecuteNonQuery(query, parameters) > 0;
        }

        // ADMIN PANEL
        public static DataTable GetAllUsers()
        {
            string query = "SELECT id_user AS nama, durasi FROM users WHERE role = 'user'";
            return GetDataTable(query);
        }

        public static bool UpdateUserDurasi(string idUser, int durasi)
        {
            string query = "UPDATE users SET durasi = @durasi WHERE id_user = @id";
            MySqlParameter[] parameters = {
                new MySqlParameter("@durasi", durasi),
                new MySqlParameter("@id", idUser)
            };
            return ExecuteNonQuery(query, parameters) > 0;
        }

        public static bool DeleteUser(string id)
        {
            string query = "DELETE FROM users WHERE id_user = @id";
            MySqlParameter[] parameters = {
                new MySqlParameter("@id", id)
            };
            return ExecuteNonQuery(query, parameters) > 0;
        }

        public static void UpdateDurasiUser(string id, int durasi)
        {
            string query = "UPDATE users SET durasi = @durasi WHERE id_user = @id";
            MySqlParameter[] parameters = {
                new MySqlParameter("@durasi", durasi),
                new MySqlParameter("@id", id)
            };
            ExecuteNonQuery(query, parameters);
        }
    }
}
