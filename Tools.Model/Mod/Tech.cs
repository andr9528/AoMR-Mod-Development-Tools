namespace Tools.Model;

public class Tech
{
    public int Id { get; set; } // EF Core primary key
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; } // e.g. "Normal" or null
    public ICollection<Effect> Effects { get; set; } = new List<Effect>();
}
