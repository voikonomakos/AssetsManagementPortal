public class ClientAssets
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int AssetId { get; set; }
    public decimal Value { get; set; }
    public DateTime AssetDate { get; set; }

    public ClientAssets() { }

    public ClientAssets(int id, int clientId, int assetId, decimal value, DateTime assetDate)
    {
        Id = id;
        ClientId = clientId;
        AssetId = assetId;
        Value = value;
        AssetDate = assetDate;
    }

    public ClientAssets(int clientId, int assetId, decimal value, DateTime assetDate)
    {
        ClientId = clientId;
        AssetId = assetId;
        Value = value;
        AssetDate = assetDate;
    }
}