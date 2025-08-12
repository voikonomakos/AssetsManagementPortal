using AssetsManagement.Configuration;
using AssetsManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

public class AuditActionsRepository : BaseRepository
{
    public AuditActionsRepository(IOptions<DatabaseConfiguration> configuration) : base(configuration) { }

    private const string TableName = "AuditActions";

    public async Task<List<AuditActions>> GetAllAuditActions()
    {
        return await GetAll<AuditActions>(TableName);
    }

    public async Task<AuditActions?> GetAuditActionById(int id)
    {
        var cmd = @$"SELECT *
                    FROM {TableName}
                    WHERE Id = @Id";

        return await Find<AuditActions>(cmd, id);
    }

    public async Task<AuditActions?> GetAuditActionsByName(string name)
    {
        var cmd = @$"SELECT *
                    FROM {TableName}
                    WHERE Name = @Name";

        return await Find<AuditActions>(cmd, new { Name = name });
    }

    public async Task InsertAuditActions(AuditActions auditAction)
    {
        var cmd = @$"INSERT INTO {TableName} (Id, Name)
                    VALUES (@Id, @Name)";

        await ExecuteAsync<AuditActions>(cmd, auditAction);
    }

    public async Task DeleteAllAuditActions()
    {
        await DeleteAll(TableName);
    }
}