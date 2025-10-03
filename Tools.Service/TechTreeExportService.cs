using System.Globalization;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Tools.Abstraction.Enum;
using Tools.Abstraction.Extensions;
using Tools.Abstraction.Interfaces;
using Tools.Model.Mod;
using Tools.Persistence;

namespace Tools.Service;

public class TechTreeExportService : ITechTreeExporter
{
    private const string RESOURCE_TRICKLE_RATE_SUBTYPE = "ResourceTrickleRate";
    private const string COST_SUBTYPE = "Cost";
    private const string DAMAGE_SUBTYPE = "Damage";

    private readonly ToolsDatabaseContext _db;

    public TechTreeExportService(ToolsDatabaseContext db)
    {
        _db = db;
    }

    public XDocument ExportToXml()
    {
        var techs = _db.Techs.Where(t => t.Effects.Any(e => e.MergeMode == MergeMode.ADD)).Include(tech => tech.Effects)
            .ThenInclude(effect => effect.Targets).Include(tech => tech.Effects).ThenInclude(effect => effect.Patterns)
            .ToList();

        var root = new XElement("techtreemods");

        foreach (Tech tech in techs)
        {
            var techElem = new XElement("tech", new XAttribute("name", tech.Name),
                new XAttribute("type", tech.Type ?? "Normal"));

            var effectsElem = new XElement("effects");

            foreach ((Effect add, Effect remove) in FindEffectPairs(tech))
            {
                AddEffectToXml(effectsElem, remove);
                AddEffectToXml(effectsElem, add);
            }

            techElem.Add(effectsElem);
            root.Add(techElem);
        }

        return new XDocument(root);
    }

    private IEnumerable<(Effect Add, Effect Remove)> FindEffectPairs(Tech tech)
    {
        var effectPairs = from add in tech.Effects
            from remove in tech.Effects
            where add.MergeMode == MergeMode.ADD && remove.MergeMode == MergeMode.REMOVE
            where MatchesPairingRule(add, remove)
            select (Add: add, Remove: remove);

        return effectPairs;
    }

    private bool MatchesPairingRule(Effect add, Effect remove)
    {
        // Resource-based pairing
        if (RequiresResourceMatching(add.Subtype) && RequiresResourceMatching(remove.Subtype))
        {
            return string.Equals(add.Resource, remove.Resource, StringComparison.OrdinalIgnoreCase);
        }

        // Action-based pairing
        if (RequiresActionMatching(add.Subtype) && RequiresActionMatching(remove.Subtype))
        {
            return string.Equals(add.Action, remove.Action, StringComparison.OrdinalIgnoreCase);
        }

        // Default: target-based pairing
        return HasMatchingTarget(add, remove);
    }

    private bool RequiresResourceMatching(string subtype)
    {
        return string.Equals(subtype, RESOURCE_TRICKLE_RATE_SUBTYPE, StringComparison.OrdinalIgnoreCase) ||
               string.Equals(subtype, COST_SUBTYPE, StringComparison.OrdinalIgnoreCase);
    }

    private bool RequiresActionMatching(string subtype)
    {
        return string.Equals(subtype, DAMAGE_SUBTYPE, StringComparison.OrdinalIgnoreCase);
    }

    private bool HasMatchingTarget(Effect add, Effect remove)
    {
        return add.Targets.Any(t1 => remove.Targets.Any(t2 => t1.Value == t2.Value));
    }

    private void AddEffectToXml(XElement effectsElement, Effect effect)
    {
        var effectElem = new XElement("effect", new XAttribute("mergeMode", effect.MergeMode.ToString().ToLower()),
            new XAttribute("subtype", effect.Subtype),
            new XAttribute("relativity", StringExtensions.ToPascalCase(effect.Relativity.ToString())));

        // Only include amount if it's meaningful (non-zero)
        if (effect.Amount != 0)
        {
            effectElem.Add(new XAttribute("amount",
                effect.Amount.ToString("0.00", CultureInfo.InvariantCulture))); // rounded to 2 decimals
        }

        if (!string.IsNullOrEmpty(effect.Action))
        {
            effectElem.Add(new XAttribute("action", effect.Action));
        }

        if (!string.IsNullOrEmpty(effect.Resource))
        {
            effectElem.Add(new XAttribute("resource", effect.Resource));
        }

        if (!string.IsNullOrEmpty(effect.Unit))
        {
            effectElem.Add(new XAttribute("unit", effect.Unit));
        }

        if (!string.IsNullOrEmpty(effect.Generator))
        {
            effectElem.Add(new XAttribute("generator", effect.Generator));
        }

        foreach (XElement targetElem in effect.Targets.Select(CreateTargetElement))
        {
            effectElem.Add(targetElem);
        }

        foreach (XElement patternElem in effect.Patterns.Select(CreatePatternElement))
        {
            effectElem.Add(patternElem);
        }

        effectsElement.Add(effectElem);
    }

    private XElement CreateTargetElement(Target target)
    {
        var targetElem = new XElement("target", new XAttribute("type", target.Type), target.Value ?? string.Empty);
        return targetElem;
    }

    private XElement CreatePatternElement(Pattern pattern)
    {
        var patternElem = new XElement("pattern", new XAttribute("type", pattern.Type),
            new XAttribute("value", pattern.Value), new XAttribute("quantity",
                pattern.Quantity.ToString("0.00", CultureInfo.InvariantCulture) // int → double-like string
            ));

        return patternElem;
    }
}
