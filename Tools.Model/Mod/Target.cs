namespace Tools.Model;

public class Target
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty; // e.g. "ProtoUnit" or "Player"
    public string? Value { get; set; } // e.g. "TownCenter"

    public int EffectId { get; set; }
    public Effect Effect { get; set; } = null!;
}
