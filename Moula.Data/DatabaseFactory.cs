﻿using NPoco;
using System.Data.SqlClient;

namespace Moula.Data
{
    /// <summary>
    /// This factory allows the database connection to be mocked and keeps connection configuration in a single location.
    /// </summary>
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
