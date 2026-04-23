using System.Globalization;
using Microsoft.Extensions.Logging;
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

    private int GetBaseDecimalPlaces(ILogger? logger = null)
    {
        string original = OriginalAmountString ?? "0";

        int index = original.IndexOf('.');
        int baseDecimals = index >= 0 ? original.Length - index - 1 : 0;

        logger?.LogDebug(
            "Decimal base | OriginalAmountString={OriginalAmountString} | BaseDecimals={BaseDecimals} | Subtype={Subtype}",
            original, baseDecimals, Subtype);

        return baseDecimals;
    }

    public int GetCalculationDecimalPlaces(ILogger? logger = null)
    {
        int baseDecimals = GetBaseDecimalPlaces(logger);
        int calculationDecimals = baseDecimals + 1;

        logger?.LogDebug(
            "Decimal calc | BaseDecimals={BaseDecimals} | CalculationDecimals={CalculationDecimals} | Subtype={Subtype}",
            baseDecimals, calculationDecimals, Subtype);

        return calculationDecimals;
    }

    public string FormatAmountForExport(ILogger? logger = null)
    {
        int baseDecimals = GetBaseDecimalPlaces(logger);
        int calculationDecimals = baseDecimals + 1;

        string formatted = Amount.ToString($"F{calculationDecimals}", CultureInfo.InvariantCulture).TrimEnd('0')
            .TrimEnd('.');

        int dotIndex = formatted.IndexOf('.');
        int currentDecimals = dotIndex >= 0 ? formatted.Length - dotIndex - 1 : 0;

        if (currentDecimals < baseDecimals)
        {
            formatted = Amount.ToString($"F{baseDecimals}", CultureInfo.InvariantCulture);
        }

        logger?.LogDebug(
            "Export format | Amount={Amount:G17} | BaseDecimals={BaseDecimals} | CalculationDecimals={CalculationDecimals}",
            Amount, baseDecimals, calculationDecimals);

        logger?.LogDebug(
            "Export result | CurrentDecimals={CurrentDecimals} | FinalValue={FinalValue} | Subtype={Subtype}",
            currentDecimals, formatted, Subtype);

        return formatted;
    }
}
