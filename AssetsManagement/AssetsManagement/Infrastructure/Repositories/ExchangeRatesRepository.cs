using Dapper;
using AssetsManagement.Configuration;
using AssetsManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Npgsql;

public class ExchangeRateRepository : BaseRepository
{
    public ExchangeRateRepository(IOptions<DatabaseConfiguration> configuration) : base(configuration) { }

    private const string TableName = "ExchangeRates";

    public async Task<List<ExchangeRates>> GetAllExchangeRates()
    {
        return await GetAll<ExchangeRates>(TableName);
    }

        public async Task<ExchangeRates?> GetExchangeRate(string currency)
    {
        var cmd = $"SELECT * FROM {TableName} WHERE Currency = @Currency";
        return await Find<ExchangeRates>(cmd, new { Currency = currency });
    }

    public async Task InsertExchangeRates(Dictionary<string, decimal> rates)
    {
        foreach (var rate in rates)
        {
            var cmd = @$"INSERT INTO {TableName} (Currency, Rate, Timestamp)
                        VALUES (@Currency, @Rate, @Timestamp)
                            ON CONFLICT (Currency) DO UPDATE
                            SET Rate = @Rate, Timestamp = @Timestamp;";

            await ExecuteAsync<ExchangeRates>(cmd, new ExchangeRates(rate.Key, rate.Value, DateTime.Today));
        }
    }

    public async Task DeleteAllExchangeRates()
    {
        await DeleteAll(TableName);
    }
}
