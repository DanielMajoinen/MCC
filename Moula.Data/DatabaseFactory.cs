using NPoco;
using System.Data.SqlClient;

namespace Moula.Data
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private string _connectionString { get; }

        public DatabaseFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDatabase CreateConnection()
        {
            return new Database(_connectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance);
        }
    }
}
