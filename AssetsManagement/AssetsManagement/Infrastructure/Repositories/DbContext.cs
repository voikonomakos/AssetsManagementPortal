using AssetsManagement.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace AssetsManagement.Infrastructure.Repositories
{
    public class DbContext
    {
        private readonly string _connectionString;

        

        public DbContext(IOptions<DatabaseConfiguration> configuration)
        {
            _connectionString = configuration.Value.DefaultConnection
                                ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null.");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
