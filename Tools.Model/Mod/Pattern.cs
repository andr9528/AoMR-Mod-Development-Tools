using Tools.Abstraction.Enum;
using Tools.Abstraction.Extensions;

namespace Tools.Model.Mod;

public class Pattern
{
    public int Id { get; set; }

    public string Type { get; set; } = string.Empty;
    public string? Value { get; set; }
    public double Quantity { get; set; }

    // Store any extra attributes (speed, radius, minradius, etc.)
    public Dictionary<string, string?> ExtraAttributes { get; set; } = new();

    public static readonly HashSet<string> KnownAttributeNames = Enum.GetValues<PatternAttribute>()
        .Select(a => a.ToXmlName()).ToHashSet(StringComparer.OrdinalIgnoreCase);

    public int EffectId { get; set; }
    public Effect Effect { get; set; } = null!;

    // Nested offset
    public double OffsetX { get; set; }
    public double OffsetY { get; set; }
    public double OffsetZ { get; set; }
}
