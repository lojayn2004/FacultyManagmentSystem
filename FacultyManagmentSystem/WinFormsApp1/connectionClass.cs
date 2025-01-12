using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseConnection
{
    using System;
    using System.Data.SqlClient;

    public static class DatabaseManager
    {
        private static readonly string connectionString = "Data Source = Lojy; " +
                "Initial Catalog = facultyManagmentSystem; Integrated Security = true ";

        public static SqlConnection GetSqlConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }

}
