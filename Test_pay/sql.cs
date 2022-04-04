using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Test_pay
{
  public class sql
    {
        public static SqlConnection sqlconnect { get; set; }
        public static void CreateCommand(string queryString,
   string connectionString)
        {

            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }

        }
    }
}
