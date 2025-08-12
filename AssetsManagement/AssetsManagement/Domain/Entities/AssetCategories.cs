public class AssetCategories
{
    public int Id { get; set; } // manually set
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public AssetCategories(int id, string name, string? description = null)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}