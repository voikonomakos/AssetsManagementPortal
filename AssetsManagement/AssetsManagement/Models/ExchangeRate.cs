namespace AssetsManagement.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;

        public List<Asset> Assets { get; set; } = new List<Asset>();
    }

    public class Asset
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime AssetDate { get; set; }

        public string AssetCategory { get; set; }

        public Asset()
        {        
            Name = string.Empty;
            Value = 0.0m;
            AssetDate = DateTime.Now;
            AssetCategory = string.Empty;    
        }

        public Asset(string name, decimal value, DateTime assetDate, string assetCategory)
        {
            Name = name;
            Value = value;
            AssetDate = assetDate;
            AssetCategory = assetCategory;
        }
    }
    public class ExchangeRate
    {
        public string Currency { get; set; } = string.Empty;
        public decimal Value { get; set; } = 0.0m;
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
