using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace WebAppii.DataBaseACC
{
    public class Connection
    {
        public NpgsqlConnection Connect()
        {
            NpgsqlConnection connection;
            connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;");
            connection.Open();
            return connection;
    }
    }
}