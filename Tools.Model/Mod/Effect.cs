using Tools.Abstraction.Enum;
using Tools.Abstraction.Extensions;

namespace Tools.Model.Mod;

public class Effect
{
    public int Id { get; set; }

    // --- Known, structured attributes (kept as-is) ---
    public MergeMode MergeMode { get; set; }
    public string Type { get; set; } = string.Empty;

    public string? Action { get; set; }
    public double Amount { get; set; }
    public string? OriginalAmountString { get; set; }

    public string Subtype { get; set; } = string.Empty;
    public string? Resource { get; set; }
    public string? Unit { get; set; }
    public string? UnitType { get; set; }
    public Relativity? Relativity { get; set; }

    public List<Target> Targets { get; set; } = new();
    public List<Pattern> Patterns { get; set; } = new();

    public int TechId { get; set; }
    public Tech Tech { get; set; } = null!;

    public Dictionary<string, string?> ExtraAttributes { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    public static readonly HashSet<string> KnownAttributeNames = Enum.GetValues<EffectAttribute>()
        .Select(a => a.ToXmlName()).ToHashSet(StringComparer.OrdinalIgnoreCase);
}
