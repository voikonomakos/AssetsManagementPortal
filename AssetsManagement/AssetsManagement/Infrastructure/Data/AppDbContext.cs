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
        /// Gets the current database connection used by Entity Framework Core.
        /// </summary>
        /// <returns>
        /// An <see cref="IDbConnection"/> representing the current database connection.
        /// </returns>
        public IDbConnection GetDbConnection()
        {
            return Database.GetDbConnection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            
            return new SqlConnection(_connectionString);
        }
    }
}
