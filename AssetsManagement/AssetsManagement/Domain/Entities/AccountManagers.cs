public class AccountManagers
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public AccountManagers() { }

    public AccountManagers(int id, string name)
    {
        Id = id;
        Name = name;
    }
}