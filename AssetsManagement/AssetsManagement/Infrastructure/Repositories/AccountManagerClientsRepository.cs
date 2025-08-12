using AssetsManagement.Configuration;
using AssetsManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

public class AccountManagerClientsRepository : BaseRepository
{
    public AccountManagerClientsRepository(IOptions<DatabaseConfiguration> configuration) : base(configuration) { }

    private const string TableName = "AccountManagerClients";

    public async Task<List<AccountManagerClients>> GetAllAccountManagerClients()
    {
        return await GetAll<AccountManagerClients>(TableName);
    }

    public async Task<List<AccountManagerClients>> GetClientsByAccountManagers(int accountManagerId)
    {
        var cmd = @$"SELECT *
                    FROM {TableName}
                    WHERE AccountManagerId = @AccountManagerId";

        return await Get<AccountManagerClients>(cmd, new { AccountManagerId = accountManagerId });
    }

    public async Task InsertAccountManagerClient(AccountManagerClients accountManagerClient)
    {
        var cmd = @$"INSERT INTO {TableName} (AccountManagerId, ClientId)
                    VALUES (@AccountManagerId, @ClientId)";

        await ExecuteAsync<AccountManagerClients>(cmd, accountManagerClient);
    }

    public async Task DeleteAllAccountManagerClients()
    {
        await DeleteAll(TableName);
    }

}