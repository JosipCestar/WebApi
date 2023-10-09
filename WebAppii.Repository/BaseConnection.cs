using Npgsql;
using System.Threading.Tasks;
using WebAppii.Repository.Common;

namespace Repository
{
    public class BaseConnection : BaseConnectionCommon
    {
        public NpgsqlConnection connection;
        private const string CONNECTION_STRING = "Host=localhost;" +
           "Port=5432;" +
           "Username=postgres;" +
           "Password=neznam555" +
           "Database=";
        public BaseConnection()
        {
            connection = new NpgsqlConnection(CONNECTION_STRING);
        }
        public void OpenConnection()
        {
            connection.OpenAsync();
        }
        public void CloseConnection()
        {
            connection.CloseAsync();
           
        }
    }
}