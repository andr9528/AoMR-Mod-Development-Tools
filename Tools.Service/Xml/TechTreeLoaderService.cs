using System.Globalization;
using System.Xml.Linq;
using Tools.Abstraction.Enum;
using Tools.Abstraction.Extensions;
using Tools.Abstraction.Interfaces;
using Tools.Model.Mod;
using Tools.Persistence;

namespace Tools.Service.Xml;

public class TechTreeLoaderService : IXmlLoader
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
                var amountAttr = (string) effectElem.Attribute(EffectAttribute.AMOUNT.ToXmlName());

                var effect = new Effect
                {
                    MergeMode = ParseEnum<MergeMode>(
                        (string) effectElem.Attribute(EffectAttribute.MERGE_MODE.ToXmlName()) ?? "remove"),
                    Type = (string) effectElem.Attribute(EffectAttribute.TYPE.ToXmlName()) ?? string.Empty,
                    Action = (string) effectElem.Attribute(EffectAttribute.ACTION.ToXmlName()),
                    Amount = double.TryParse(amountAttr, NumberStyles.Float | NumberStyles.AllowThousands,
                        CultureInfo.InvariantCulture, out double amt)
                        ? amt
                        : 0,
                    OriginalAmountString = amountAttr,
                    Subtype = (string) effectElem.Attribute(EffectAttribute.SUBTYPE.ToXmlName()) ?? string.Empty,
                    Resource = (string) effectElem.Attribute(EffectAttribute.RESOURCE.ToXmlName()),
                    Unit = (string) effectElem.Attribute(EffectAttribute.UNIT.ToXmlName()),
                    UnitType = (string) effectElem.Attribute(EffectAttribute.UNIT_TYPE.ToXmlName()),
                    Relativity = ParseRelativity((string) effectElem.Attribute(EffectAttribute.RELATIVITY.ToXmlName())),
                };

                foreach (XAttribute attr in effectElem.Attributes())
                {
                    string name = attr.Name.LocalName;
                    if (!Effect.KnownAttributeNames.Contains(name))
                    {
                        effect.ExtraAttributes[name] = attr.Value;
                    }
                }

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
                Type = (string) patternElem.Attribute(PatternAttribute.TYPE.ToXmlName()) ?? string.Empty,
                Value = (string) patternElem.Attribute(PatternAttribute.VALUE.ToXmlName()) ?? string.Empty,
                Quantity = double.TryParse((string) patternElem.Attribute(PatternAttribute.QUANTITY.ToXmlName()),
                    NumberStyles.Float, CultureInfo.InvariantCulture, out double qty)
                    ? qty
                    : 0,
            };

            foreach (XAttribute attr in patternElem.Attributes())
            {
                string name = attr.Name.LocalName;
                if (!Pattern.KnownAttributeNames.Contains(name))
                {
                    pattern.ExtraAttributes[name] = attr.Value;
                }
            }

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
                Type = (string) targetElem.Attribute(TargetAttribute.TYPE.ToXmlName()) ?? string.Empty,
                Value = targetElem.Value?.Trim(),
            };

            foreach (XAttribute attr in targetElem.Attributes())
            {
                string name = attr.Name.LocalName;
                if (!Pattern.KnownAttributeNames.Contains(name))
                {
                    target.ExtraAttributes[name] = attr.Value;
                }
            }

            effect.Targets.Add(target);
        }
    }

    private T ParseEnum<T>(string value) where T : struct
    {
        return Enum.TryParse(value, true, out T result) ? result : default;
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
