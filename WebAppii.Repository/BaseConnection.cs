using Npgsql;
using System.Management.Instrumentation;
using System.Threading.Tasks;
using WebAppii.Repository.Common;

namespace Repository
{
    public class BaseConnection : IBaseConnection
    {
        public NpgsqlConnection connectionString;
        private const string CONNECTION_STRING = "Host=localhost;" +
           "Port=5432;" +
           "Username=postgres;" +
           "Password=neznam555" +
           "Database=";
        public BaseConnection()
        {
            connectionString = new NpgsqlConnection(CONNECTION_STRING);
        }
        public void OpenConnection()
        {
            connectionString.OpenAsync();
        }
        public void CloseConnection()
        {
            connectionString.CloseAsync();
           
        }
    }
}