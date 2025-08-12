
using AssetsManagement.Configuration;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace AssetsManagement.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected string ConnectionString;

        protected BaseRepository(IOptions<DatabaseConfiguration> configuration)
        {
            ConnectionString = configuration.Value.DefaultConnection;
        }

        public async Task<List<T>> GetAll<T>(string tableName)
        {
            string query = $"SELECT * FROM {tableName}";

            return await Get<T>(query);
        }

        protected async Task<List<T>> Get<T>(string query, object? parameters = null)
        {
            return await WithConnection(async conn =>
            {
                var data = await conn.QueryAsync<T>(query, parameters);

                return data.ToList();
            });
        }

        protected async Task<T?> Find<T>(string query, int id)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QuerySingleOrDefaultAsync<T>(query, new { id });
            });
        }

        protected async Task<T?> Find<T>(string query, object? parameters = null)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QuerySingleOrDefaultAsync<T>(query, parameters);
            });
        }

        protected async Task ExecuteAsync<T>(string cmdText, T data)
        {
            await WithConnection(async conn => { await conn.ExecuteAsync(cmdText, data); });
        }

        protected async Task ExecuteAsync<T>(string cmdText, List<T> data)
        {
            await WithConnection(async conn => { await conn.ExecuteAsync(cmdText, data); });
        }

        protected async Task ExecuteAsync(string cmdText)
        {
            await WithConnection(async conn => { await conn.ExecuteAsync(cmdText); });
        }

        protected async Task DeleteAll(string tableName)
        {
            await ExecuteAsync($"DELETE FROM {tableName}");
        }

        protected async Task<T> WithConnection<T>(Func<NpgsqlConnection, Task<T>> getData)
        {
            try
            {
                await using var connection = new NpgsqlConnection(ConnectionString);
                await connection.OpenAsync();
                var result = await getData(connection);
                await connection.CloseAsync();
                return result;
            }
            catch (TimeoutException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception {ex.Message}", ex);
            }
        }

        protected async Task WithConnection(Func<NpgsqlConnection, Task> action)
        {
            try
            {
                await using var connection = new NpgsqlConnection(ConnectionString);
                await connection.OpenAsync();
                await action(connection);
                await connection.CloseAsync();
            }
            catch (TimeoutException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception {ex.Message}", ex);
            }
        }
    }
}
=======
﻿using AssetsManagement.Configuration;
using AssetsManagement.Infrastructure.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace AssetsManagement.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        private readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAll<T>(string tableName)
        {
            string query = $"SELECT * FROM {tableName}";

            return await Get<T>(query);
        }

        protected async Task<List<T>> Get<T>(string query, object? parameters = null)
        {
            return await WithConnection(async conn =>
            {
                var data = await conn.QueryAsync<T>(query, parameters);

                return data.ToList();
            });
        }

        protected async Task<T?> Find<T>(string query, int id)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QuerySingleOrDefaultAsync<T>(query, new { id });
            });
        }

        protected async Task<T?> Find<T>(string query, object? parameters = null)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QuerySingleOrDefaultAsync<T>(query, parameters);
            });
        }

        protected async Task ExecuteAsync<T>(string cmdText, T data)
        {
            await WithConnection(async conn => { await conn.ExecuteAsync(cmdText, data); });
        }

        protected async Task ExecuteAsync<T>(string cmdText, List<T> data)
        {
            await WithConnection(async conn => { await conn.ExecuteAsync(cmdText, data); });
        }

        protected async Task ExecuteAsync(string cmdText)
        {
            await WithConnection(async conn => { await conn.ExecuteAsync(cmdText); });
        }

        protected async Task DeleteAll(string tableName)
        {
            await ExecuteAsync($"DELETE FROM {tableName}");
        }

        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using IDbConnection connection = _dbContext.CreateNewConnection();
                connection.Open();
                var result = await getData(connection);
                connection.Close();
                return result;
            }
            catch (TimeoutException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception {ex.Message}", ex);
            }
        }

        protected async Task WithConnection(Func<IDbConnection, Task> action)
        {
            try
            {
                using IDbConnection connection = _dbContext.CreateNewConnection();
                connection.Open();
                await action(connection);
                connection.Close();
            }
            catch (TimeoutException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception {ex.Message}", ex);
            }
        }
    }
}
