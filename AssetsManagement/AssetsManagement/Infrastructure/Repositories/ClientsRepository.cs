using AssetsManagement.Configuration;
using AssetsManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

public class ClientsRepository : BaseRepository
{
    public ClientsRepository(IOptions<DatabaseConfiguration> configuration) : base(configuration) { }

    private const string TableName = "Clients";

    public async Task<List<Clients>> GetAllClients()
    {
        return await GetAll<Clients>(TableName);
    }

    public async Task<Clients?> GetClient(string name)
    {
        var cmd = $"SELECT * FROM {TableName} WHERE Name = @Name";
        return await Find<Clients>(cmd, new { Name = name });
    }

    public async Task InsertClient(Clients client)
    {
        var cmd = @$"INSERT INTO {TableName} (Name)
                        VALUES (@Name)";

        await ExecuteAsync<Clients>(cmd, client);
    }

    public async Task DeleteAllClients()
    {
        await DeleteAll(TableName);
    }

}