using Tools.Abstraction.Enum;
using Tools.Abstraction.Extensions;

namespace Tools.Model.Mod;

public class Target
{
    public int Id { get; set; }

    public string Type { get; set; } = string.Empty;
    public string? Value { get; set; }

    // Store any extra attributes on target elements
    public Dictionary<string, string?> ExtraAttributes { get; set; } = new();

    public static readonly HashSet<string> KnownAttributeNames = Enum.GetValues<TargetAttribute>()
        .Select(a => a.ToXmlName()).ToHashSet(StringComparer.OrdinalIgnoreCase);

    public int EffectId { get; set; }
    public Effect Effect { get; set; } = null!;
}
