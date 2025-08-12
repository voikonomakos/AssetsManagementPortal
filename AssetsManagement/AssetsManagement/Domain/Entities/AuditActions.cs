public class AuditActions
{
    public int Id { get; set; } // set manually
    public string Name { get; set; } = string.Empty;

    public AuditActions(int id, string name)
    {
        Id = id;
        Name = name;
    }
}