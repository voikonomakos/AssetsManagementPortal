public class AccountManagerClients
{
    public int Id { get; set; }
    public int AccountManagerId { get; set; }
    public int ClientId { get; set; }

    // constructor with id for GET methods
    public AccountManagerClients(int id, int accountManagerId, int clientId)
    {
        Id = id;
        AccountManagerId = accountManagerId;
        ClientId = clientId;
    }

    // constructor without id for INSERT/UPDATE methods
    public AccountManagerClients(int accountManagerId, int clientId)
    {
        AccountManagerId = accountManagerId;
        ClientId = clientId;
    }
}