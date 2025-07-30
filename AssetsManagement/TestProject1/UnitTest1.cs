using System.Text;
using System.Xml.Serialization;
using Bogus;
using Xunit.Abstractions;

namespace TestProject1
{
    public class UnitTest1 : TestBase
    {
        public UnitTest1(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void Test1()
        {
            // fake funds generator
            var fundsFaker = new Faker<Fund>()
            .RuleFor(f => f.Name, f => f.Company.CompanyName())
            .RuleFor(f => f.Amount, f => f.Finance.Amount(1000, 10000))
            .RuleFor(f => f.FundDate, f => f.Date.Past(1));

            var funds = fundsFaker.Generate(2);

            // fake clients generator
            var clientsFaker = new Faker<Client>() 
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.AccountCurrency, f => f.Finance.Currency().Code)
            .RuleFor(c => c.AccountType, f => f.PickRandom<AccountType>())
            .RuleFor(c => c.Funds, f => fundsFaker.Generate(2));

            var clients = clientsFaker.Generate(12);

            // divide clients by chunks (groups of 10)
            var clientChunks = clients
                .Select((client, index) => new { client, index })
                .GroupBy(x => x.index / 10)
                .Select(g => g.Select(x => x.client).ToList())
                .ToList();

            int fileIndex = 1; // will be used for naming
            var clientFileNames = new List<string>(); // used to track what the file names for the clients are

            foreach (var chunk in clientChunks) // loop through each chunk and write it to a separate xml file
            {
                var serializer = new XmlSerializer(typeof(List<Client>), new XmlRootAttribute("Clients")); // creates serializer to serialize list of clients with Clients being the root
                using var stream = new MemoryStream();                      // creates an in-memory stream (like a virutal file) which will be disposed of when we're done
                serializer.Serialize(stream, chunk);                        // serializes the current chunk to the stream
                string xml = Encoding.UTF8.GetString(stream.ToArray());     // converts the bytes in the stream to readable xml?  

                string fileName = $"clients_part_{fileIndex}.xml";          // file name gets incremented with every chunk
                File.WriteAllText(fileName, xml);                           // saves the xml string to the file
                this.Log($"Written: {fileName}");                           // logging
                clientFileNames.Add(fileName);                              // saves the name of the file to the list

                Assert.False(string.IsNullOrWhiteSpace(xml));               // making sure the xml is not empty
                fileIndex++;
            }

            var managerIndex = new ManagerIndex
            {
                FirstName = "Joe",
                LastName = "Doe",
                Clients = clients
            };

            var indexSerializer = new XmlSerializer(typeof(ManagerIndex));
            using var indexStream = new MemoryStream();
            indexSerializer.Serialize(indexStream, managerIndex);
            string indexXml = Encoding.UTF8.GetString(indexStream.ToArray());

            File.WriteAllText("manager_index.xml", indexXml);   // Save merged client data
            this.Log("Written: manager_index.xml");
            this.Log(indexXml);

            Assert.False(string.IsNullOrWhiteSpace(indexXml));

        }
    }
}

public class Fund
{
    public required string Name { get; set; }
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

public enum AccountType
{
    Standard,
    Professional
}

public class Client
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string AccountCurrency { get; set; }
    public required AccountType AccountType { get; set; }
    public required List<Fund> Funds { get; set; }

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

public class ManagerIndex
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    [XmlArray("Clients")]
    [XmlArrayItem("Client")]
    public List<Client> Clients { get; set; } = new();
}