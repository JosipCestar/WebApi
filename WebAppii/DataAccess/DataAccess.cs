using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace WebAppii.DataAccess
{
    public class DataAccess
    {
        private string ConnectionString { get; set; }


        public DataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void ExecuteNonQuery(string query)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }   
    }
}