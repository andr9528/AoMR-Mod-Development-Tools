namespace Tools.Model.Mod;

public class Pattern
{
    public int Id { get; set; }

    public string Type { get; set; } = string.Empty; // e.g. "Unit"
    public string Value { get; set; } = string.Empty; // e.g. "SomeUnit"

    // quantity written as double in XML (1.00, 5.00 etc.)
    public double Quantity { get; set; }

    // Scatter-specific
    public double Speed { get; set; }
    public double Radius { get; set; }
    public double MinRadius { get; set; }

    // Nested offset
    public double OffsetX { get; set; }
    public double OffsetY { get; set; }
    public double OffsetZ { get; set; }

    public int EffectId { get; set; }
    public Effect Effect { get; set; } = null!;
}
