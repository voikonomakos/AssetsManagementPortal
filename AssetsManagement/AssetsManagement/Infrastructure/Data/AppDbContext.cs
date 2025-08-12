using AssetsManagement.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System.Data;

namespace AssetsManagement.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext
    {
        private readonly string _connectionString;
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

            // Extract the connection string from the options
            var extension = options.Extensions
                .OfType<RelationalOptionsExtension>()
                .FirstOrDefault();

            _connectionString = extension?.ConnectionString
                ?? throw new InvalidOperationException("Connection string not found in DbContext options.");


        }

        /// <summary>
        /// Gets the current database connection for this DBContext, used by Entity Framework Core.
        /// If the connection is not open, it will be opened automatically.
        /// </summary>
        /// <remarks>
        /// This connection should not be disposed because it was created by Entity Framework.
        /// The application is responsible for disposing this connection.
        /// </remarks>
        /// <returns>
        /// An <see cref="IDbConnection"/> representing the current database connection.
        /// </returns>
        public IDbConnection GetAndOpenDbConnection()
        {
            var conn=  Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            return conn;
        }


        /// <summary>
        /// Create new connection to the database using the connection string.
        /// Create a new connection only when you need to execute a command or query outside the context of the DbContext,
        /// otherwiese use the dbContext's connection.
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateNewConnection()
        {
            
            return new SqlConnection(_connectionString);
        }
    }
}
