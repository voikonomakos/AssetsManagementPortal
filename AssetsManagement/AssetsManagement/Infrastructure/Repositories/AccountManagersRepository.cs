using AssetsManagement.Configuration;
using AssetsManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

public class AccountManagersRepository : BaseRepository
{
    public AccountManagersRepository(IOptions<DatabaseConfiguration> configuration) : base(configuration) { }

    private const string TableName = "AccountManagers";

    public async Task<List<AccountManagers>> GetAllAccountManagers()
    {
        return await GetAll<AccountManagers>(TableName);
    }

    public async Task<AccountManagers?> GetAccountManager(string name)
    {
        var cmd = @$"SELECT *
                    FROM {TableName}
                    WHERE Name = @Name";
        return await Find<AccountManagers>(cmd, new { Name = name });
    }

    public async Task InsertAccountManager(AccountManagers accountManager)
    {
        var cmd = @$"INSERT INTO {TableName} (Name)
                    VALUES (@Name)";

        await ExecuteAsync<AccountManagers>(cmd, accountManager);
    }

    public async Task DeleteAllAccountManagers()
    {
        await DeleteAll(TableName);
    }
}