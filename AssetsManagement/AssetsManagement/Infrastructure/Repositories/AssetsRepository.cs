using AssetsManagement.Configuration;
using AssetsManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

public class AssetsRepository : BaseRepository
{
    public AssetsRepository(IOptions<DatabaseConfiguration> configuration) : base(configuration) { }

    private const string TableName = "Assets";

    public async Task<List<Assets>> GetAllAssets()
    {
        return await GetAll<Assets>(TableName);
    }

    public async Task<List<Assets>> GetAssetsByCategoryId(int categoryId)
    {
        var cmd = @$"SELECT *
                    FROM {TableName}
                    WHERE CategoryId = @CategoryId";

        return await Get<Assets>(cmd, new { CategoryId = categoryId });
    }

    public async Task<Assets?> GetAsset(string name)
    {
        var cmd = @$"SELECT *
                    FROM {TableName}
                    WHERE Name = @Name";

        return await Find<Assets>(cmd, new { Name = name });
    }

    public async Task InsertAsset(Assets asset)
    {
        var cmd = @$"INSERT INTO {TableName} (Name, CategoryId)
                    VALUES (@Name, @CategoryId)";

        await ExecuteAsync<Assets>(cmd, asset);
    }

    public async Task DeleteAllAssets()
    {
        await DeleteAll(TableName);
    }
}