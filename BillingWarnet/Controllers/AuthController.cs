using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingWarnet.Helpers;

namespace BillingWarnet.Controllers
{
    public static class AuthController
    {
        public static (string role, int durasi)? Login(string id, string password)
        {
            return DatabaseHelper.Login(id, password);
        }

        public static void LogLogin(string id)
        {
            DatabaseHelper.LogLogin(id);
        }

        public static bool IsUserExist(string id)
        {
            return DatabaseHelper.IsUserExist(id);
        }

        public static bool Register(string id, string password)
        {
            if (IsUserExist(id)) return false;
            return DatabaseHelper.RegisterUser(id, password);
        }
    }
}