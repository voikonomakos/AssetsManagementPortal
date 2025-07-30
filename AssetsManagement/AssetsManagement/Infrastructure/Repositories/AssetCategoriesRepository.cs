using AssetsManagement.Configuration;
using AssetsManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

public class AssetCategoriesRepository : BaseRepository
{
    public AssetCategoriesRepository(IOptions<DatabaseConfiguration> configuration) : base(configuration) { }

    private const string TableName = "AssetCategories";

    public async Task<List<AssetCategories>> GetAllAssetCategories()
    {
        return await GetAll<AssetCategories>(TableName);
    }

    public async Task<AssetCategories?> GetAssetCategoryById(int id)
    {
        var cmd = @$"SELECT *
                    FROM {TableName}
                    WHERE Id = @Id";

        return await Find<AssetCategories>(cmd, id);
    }

    public async Task<AssetCategories?> GetAssetCategoryByName(string name)
    {
        var cmd = @$"SELECT *
                    FROM {TableName}
                    WHERE Name = @Name";

        return await Find<AssetCategories>(cmd, new { Name = name });
    }

    public async Task InsertAssetCategory(AssetCategories assetCategory)
    {
        var cmd = @$"INSERT INTO {TableName} (Id, Name, Description)
                    VALUES (@Id, @Name, @Description)";

        await ExecuteAsync<AssetCategories>(cmd, assetCategory);
    }

    public async Task DeleteAllAssetCategories()
    {
        await DeleteAll(TableName);
    }
}