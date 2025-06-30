using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BillingWarnet.Helpers;

namespace BillingWarnet.Controllers
{
    public static class UserController
    {
        public static DataTable GetAllUsers()
        {
            return DatabaseHelper.GetAllUsers();
        }

        public static bool AddUser(string id, int durasi)
        {
            if (DatabaseHelper.RegisterUser(id, "123456"))
            {
                DatabaseHelper.UpdateDurasiUser(id, durasi);
                return true;
            }
            return false;
        }

        public static bool UpdateDurasi(string id, int durasi)
        {
            return DatabaseHelper.UpdateUserDurasi(id, durasi);
        }

        public static bool DeleteUser(string id)
        {
            return DatabaseHelper.DeleteUser(id);
        }

        public static void UpdateDurasiLangsung(string id, int durasi)
        {
            DatabaseHelper.UpdateDurasiUser(id, durasi);
        }
    }
}
