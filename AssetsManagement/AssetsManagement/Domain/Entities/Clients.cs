public class Clients
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Clients() { }

    public Clients(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
