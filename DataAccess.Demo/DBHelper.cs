using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Demo
{
    public static class DBHelper
    {
        public static string sqlConn = "data source=DESKTOP-IFRSV3F;initial catalog=ManagerProduct1;user id=sa;password=123456";
        static SqlConnection conn = null;
        public static SqlConnection GetConnection()
        {
            conn = new SqlConnection(sqlConn);
            if(conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn;
        }
    }
}
