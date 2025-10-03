using System.Globalization;
using System.Xml.Linq;
using Tools.Abstraction.Enum;
using Tools.Abstraction.Interfaces;
using Tools.Model.Mod;
using Tools.Persistence;

namespace Tools.Service;

public class TechTreeLoaderService : ITechTreeLoader
{
    private readonly ToolsDatabaseContext _db;

    public TechTreeLoaderService(ToolsDatabaseContext db)
    {
        _db = db;
    }

    public async Task LoadFromFileAsync(string xmlPath)
    {
        if (!File.Exists(xmlPath))
        {
            throw new FileNotFoundException($"TechTree file not found: {xmlPath}");
        }

        XDocument doc = XDocument.Load(xmlPath);

        var techs = new List<Tech>();

        foreach (XElement techElem in doc.Descendants("tech"))
        {
            var tech = new Tech
            {
                Name = (string) techElem.Attribute("name") ?? string.Empty,
                Type = (string) techElem.Attribute("type"),
            };

            foreach (XElement effectElem in techElem.Descendants("effect"))
            {
                var effect = new Effect
                {
                    MergeMode = ParseEnum<MergeMode>((string) effectElem.Attribute("mergeMode") ?? "remove"),
                    Type = (string) effectElem.Attribute("type") ?? string.Empty,
                    Action = (string) effectElem.Attribute("action"),
                    Amount = double.TryParse((string) effectElem.Attribute("amount"),
                        NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double amt)
                        ? amt
                        : 0,
                    Subtype = (string) effectElem.Attribute("subtype") ?? string.Empty,
                    Resource = (string) effectElem.Attribute("resource"),
                    Unit = (string) effectElem.Attribute("unit"),
                    UnitType = (string) effectElem.Attribute("unittype"),
                    ArmorType = (string) effectElem.Attribute("armortype"),
                    IgnoreRally = (string) effectElem.Attribute("ignorerally"),
                    Generator = (string) effectElem.Attribute("generator"),
                    Relativity = ParseRelativity((string) effectElem.Attribute("relativity")),
                };

                ParseEffectTargets(effectElem, effect);
                ParseEffectPatterns(effectElem, effect);

                tech.Effects.Add(effect);
            }

            techs.Add(tech);
        }

        // Insert into DB
        _db.Techs.RemoveRange(_db.Techs); // Clear old
        await _db.SaveChangesAsync();

        await _db.Techs.AddRangeAsync(techs);
        await _db.SaveChangesAsync();
    }

    private static void ParseEffectPatterns(XElement effectElem, Effect effect)
    {
        foreach (XElement patternElem in effectElem.Descendants("pattern"))
        {
            var pattern = new Pattern
            {
                Type = (string) patternElem.Attribute("type") ?? string.Empty,
                Value = (string) patternElem.Attribute("value") ?? string.Empty,
                Quantity = double.TryParse((string) patternElem.Attribute("quantity"), NumberStyles.Float,
                    CultureInfo.InvariantCulture, out double qty)
                    ? qty
                    : 0,
                Speed = ParseDouble(patternElem.Attribute("speed")),
                Radius = ParseDouble(patternElem.Attribute("radius")),
                MinRadius = ParseDouble(patternElem.Attribute("minradius")),
            };

            XElement? offsetElem = patternElem.Element("offset");
            if (offsetElem != null)
            {
                pattern.OffsetX = ParseDouble(offsetElem.Attribute("x"));
                pattern.OffsetY = ParseDouble(offsetElem.Attribute("y"));
                pattern.OffsetZ = ParseDouble(offsetElem.Attribute("z"));
            }

            effect.Patterns.Add(pattern);
        }
    }

    private static void ParseEffectTargets(XElement effectElem, Effect effect)
    {
        foreach (XElement targetElem in effectElem.Descendants("target"))
        {
            var target = new Target
            {
                Type = (string) targetElem.Attribute("type") ?? string.Empty,
                Value = targetElem.Value?.Trim(),
            };
            effect.Targets.Add(target);
        }
    }

    private T ParseEnum<T>(string value) where T : struct
    {
        return Enum.TryParse<T>(value, true, out T result) ? result : default;
    }

    private static double ParseDouble(XAttribute? attr)
    {
        return attr != null &&
               double.TryParse(attr.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out double d)
            ? d
            : 0.0;
    }

    private Relativity ParseRelativity(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Relativity.NULL;
        }

        return value.ToLowerInvariant() switch
        {
            "absolute" => Relativity.ABSOLUTE,
            "percent" => Relativity.PERCENT,
            "basepercent" => Relativity.BASE_PERCENT,
            "assign" => Relativity.ASSIGN,
            var _ => Relativity.NULL,
        };
    }
}
