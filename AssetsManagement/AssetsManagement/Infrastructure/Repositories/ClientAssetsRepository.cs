using AssetsManagement.Configuration;
using AssetsManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

public class ClientAssetsRepository : BaseRepository
{
    public ClientAssetsRepository(IOptions<DatabaseConfiguration> configuration) : base(configuration) { }

    private const string TableName = "ClientAssets";

    public async Task<List<ClientAssets>> GetAllClientAssets()
    {
        return await GetAll<ClientAssets>(TableName);
    }

    // returns all the assets owned by a specific client
    public async Task<List<ClientAssets>> GetClientAssetsByClient(int clientId)
    {
        var cmd = @$"SELECT *
                    FROM {TableName}
                    WHERE ClientId = @ClientId";

        return await Get<ClientAssets>(cmd, new { ClientId = clientId });
    }

    // returns all the clients that own a specific asset
    public async Task<List<ClientAssets>> GetAllClientsByAsset(int assetId)
    {
        var cmd = @$"SELECT *
                    FROM {TableName}
                    WHERE AssetId = @AssetId";

        return await Get<ClientAssets>(cmd, new { AssetId = assetId });
    }

    public async Task InsertClientAsset(ClientAssets clientAsset)
    {
        var cmd = @$"INSERT INTO {TableName} (ClientId, AssetId, Value, AssetDate)
                    VALUES (@ClientId, @AssetId, @Value, @AssetDate)";

        await ExecuteAsync<ClientAssets>(cmd, clientAsset);
    }

    public async Task DeleteAllClientAssets()
    {
        await DeleteAll(TableName);
    }
}