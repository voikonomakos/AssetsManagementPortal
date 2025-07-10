using AssetsManagement.Models;

namespace AssetsManagement
{
    public class Db
    {
        public required List<ExchangeRate> Rates { get; set; }
        public required List<Client> Clients { get; set; }

        public required List<Currency> Currencies { get; set; }

        public required List<AccountManager> AccountManagers { get; set; }
        public Db()
        {
            Init();
        }

        public void Init()
        {
            Rates = new List<ExchangeRate>();
            Rates.Add(new ExchangeRate { Currency = "USD", Value = 1.0m, LastUpdated = DateTime.Now });
            Rates.Add(new ExchangeRate { Currency = "EUR", Value = 0.85m, LastUpdated = DateTime.Now });
            Rates.Add(new ExchangeRate { Currency = "GBP", Value = 0.75m, LastUpdated = DateTime.Now });

            Clients = new List<Client>();
            var clientA = new Client
            {
                Id = 1,
                Name = "George",
                LastName = "Tosounidou",
                Assets = new List<Asset>
                {
                    new Asset("BTC", 2, DateTime.Now, "CRYPTOS"),
                    new Asset("ETH", 2, DateTime.Now, "CRYPTOS")
                },
                CurrenciesId = new List<int> { 1, 2, 3 },
                AccountManagerId = 1
            
             };
                
            Clients.Add(clientA);

            Currencies = new List<Currency>
            {
                new Currency { Id = 1, Name = "USD" },
                new Currency { Id = 2, Name = "EUR" },
                new Currency { Id = 3, Name = "GBP" }
            };

            AccountManagers = new List<AccountManager>
            {
                new AccountManager { Id = 1, Name = "John", Surname = "Doe" },
                new AccountManager { Id = 2, Name = "Jane", Surname = "Smith" }
            };
        }

        public void UpdateRate(string currency, decimal value)
        {
            foreach (ExchangeRate rate in Rates)
            {
                if(rate.Currency == currency)
                {
                    rate.Value = value;
                    rate.LastUpdated = DateTime.Now;
                }
            }
        }


        // add method id => max items in Clients List + 1
        public ExchangeRate GetRate(string currency)
        {
            return Rates.Where (x => x.Currency == currency).First();           
        }
    }
}
