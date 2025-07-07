using System.Text;
using System.Xml.Serialization;
using Bogus;
using Xunit.Abstractions;

namespace TestProject1
{
    public class UnitTest1 : TestBase
    {
        public UnitTest1(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test1()
        {
            var fundsFaker = new Faker<Fund>()
                .RuleFor(f => f.Name, f => f.Company.CompanyName())
                .RuleFor(f => f.Amount, f => f.Finance.Amount(1000, 100000))
                .RuleFor(f => f.FundDate, f => f.Date.Past(1));
            List<Fund> funds = fundsFaker.Generate(10);

            var clientsFaker = new Faker<Client>()
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.AccountCurrency, f => f.Finance.Currency().Code)
                .RuleFor(c => c.AccountType, f => f.PickRandom<AccountType>())
                .RuleFor(c => c.Funds, f => funds);
            List<Client> clients = clientsFaker.Generate(5);

            Manager manager = new Manager("Joe", "Doe", clients);
            Assert.NotNull(manager);

            // XML serialization
            var serializer = new XmlSerializer(typeof(Manager));
            using var stream = new MemoryStream();
            serializer.Serialize(stream, manager);
            string xml = Encoding.UTF8.GetString(stream.ToArray());

            this.Log(xml);
            File.WriteAllText("report.xml", xml);
            // Optionally, assert xml is not empty
            Assert.False(string.IsNullOrWhiteSpace(xml));
        }
    }

    public enum AccountType
    {
        Standard,
        Professional
    }

    public class Fund
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime FundDate { get; set; }

        public Fund() { }

        public Fund(string name, decimal amount, DateTime fundDate)
        {
            Name = name;
            Amount = amount;
            FundDate = fundDate;
        }
    }

    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountCurrency { get; set; }
        public AccountType AccountType { get; set; }
        public List<Fund> Funds { get; set; }

        public Client() { }

        public Client(string firstName, string lastName, string accountCurrency, AccountType accountType, List<Fund> funds)
        {
            FirstName = firstName;
            LastName = lastName;
            AccountCurrency = accountCurrency;
            AccountType = accountType;
            Funds = funds;
        }
    }

    public class Manager
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Client> Clients { get; set; }

        public Manager() { }

        public Manager(string firstName, string lastName, List<Client> clients)
        {
            FirstName = firstName;
            LastName = lastName;
            Clients = clients;
        }
    }

    public class Company
    {
        public string Name { get; set; }
        public List<Manager> Managers { get; set; }
    }
}
