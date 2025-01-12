using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public static class UserSession
    {
        public static string email { get; private set; }
        public static string Role { get; private set; }

        public static int userId;
        public static void SetUserSession(string email, string role)
        {
            UserSession.email = email;
            Role = role;
        }
    }

}
