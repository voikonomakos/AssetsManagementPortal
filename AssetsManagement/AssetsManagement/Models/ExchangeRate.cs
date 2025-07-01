namespace AssetsManagement.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty; 
        // currency string, accountmanager string
        public List<Asset> Assets { get; set; } = new List<Asset>();
        public List<int> CurrenciesId { get; set; } = new List<int>();
        public int AccountManagerId { get; set; } 
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

    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set;}

        public Currency()
        {
            Name = string.Empty;
        }
        public Currency(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class AccountManager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public AccountManager()
        {
            Name = string.Empty;
            Surname = string.Empty;
        }
        
        public AccountManager(int id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }
    }
}
