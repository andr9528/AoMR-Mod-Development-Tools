namespace Tools.Model.Mod;

public class Pattern
{
    public int Id { get; set; }

    public string Type { get; set; } = string.Empty; // e.g. "Unit"
    public string Value { get; set; } = string.Empty; // e.g. "SomeUnit"
    public int Quantity { get; set; } // this is the property we need to multiply

    public int EffectId { get; set; }
    public Effect Effect { get; set; } = null!;
}
