using System.Globalization;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Tools.Abstraction.Enum;
using Tools.Abstraction.Extensions;
using Tools.Abstraction.Interfaces;
using Tools.Model.Mod;
using Tools.Persistence;

// ReSharper disable StringLiteralTypo

namespace Tools.Service;

public class TechTreeExportService : ITechTreeExporter
{
    private const string COST_SUBTYPE = "Cost";

    private const string RESOURCE_TRICKLE_RATE_SUBTYPE = "ResourceTrickleRate";
    private const string RESOURCE_RETURN_SUBTYPE = "ResourceReturn";
    private const string COST_BUILDING_TECHS_SUBTYPE = "CostBuildingTechs";

    private const string GODPOWER_ROF_FACTOR_SUBTYPE = "GodPowerROFFactor";
    private const string GODPOWER_COST_FACTOR_SUBTYPE = "GodPowerCostFactor";
    private const string LOS_SUBTYPE = "LOS";
    private const string MAXIMUM_VELOCITY_SUBTYPE = "MaximumVelocity";

    private const string DAMAGE_SUBTYPE = "Damage";
    private const string MAXIMUM_RANGE_SUBTYPE = "MaximumRange";

    private const string WORK_RATE_SUBTYPE = "WorkRate";
    private const string ON_HIT_EFFECT_SUBTYPE = "OnHitEffect";
    private const string ON_HIT_EFFECT_ATTACH_BONE_SUBTYPE = "OnHitEffectAttachBone";

    private const string CREATE_UNIT_TYPE = "CreateUnit";

    private static readonly Dictionary<string, Func<Effect, Effect, bool>> PairingRules =
        new(StringComparer.OrdinalIgnoreCase)
        {
            // Cost: match on resource + target
            [COST_SUBTYPE] = (add, remove) =>
                string.Equals(add.Resource, remove.Resource, StringComparison.OrdinalIgnoreCase) &&
                HasMatchingTarget(add, remove),

            // ResourceReturn: match on resource + target
            [RESOURCE_RETURN_SUBTYPE] = (add, remove) =>
                string.Equals(add.Resource, remove.Resource, StringComparison.OrdinalIgnoreCase) &&
                HasMatchingTarget(add, remove),

            // ResourceTrickleRate / ResourceReturn / CostBuildingTechs: match on resource
            [COST_BUILDING_TECHS_SUBTYPE] = (add, remove) =>
                string.Equals(add.Resource, remove.Resource, StringComparison.OrdinalIgnoreCase),
            [RESOURCE_TRICKLE_RATE_SUBTYPE] = (add, remove) =>
                string.Equals(add.Resource, remove.Resource, StringComparison.OrdinalIgnoreCase),

            // Damage / MaximumRange: match on action + target
            [DAMAGE_SUBTYPE] = (add, remove) =>
                string.Equals(add.Action, remove.Action, StringComparison.OrdinalIgnoreCase) &&
                HasMatchingTarget(add, remove),
            [MAXIMUM_RANGE_SUBTYPE] = (add, remove) =>
                string.Equals(add.Action, remove.Action, StringComparison.OrdinalIgnoreCase) &&
                HasMatchingTarget(add, remove),

            // WorkRate: match on action and unit type
            [WORK_RATE_SUBTYPE] = (add, remove) =>
                string.Equals(add.Action, remove.Action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(add.UnitType, remove.UnitType, StringComparison.OrdinalIgnoreCase),

            // OnHitEffect / OnHitEffectAttachBone: match on action
            [ON_HIT_EFFECT_SUBTYPE] = (add, remove) =>
                string.Equals(add.Action, remove.Action, StringComparison.OrdinalIgnoreCase),
            [ON_HIT_EFFECT_ATTACH_BONE_SUBTYPE] = (add, remove) =>
                string.Equals(add.Action, remove.Action, StringComparison.OrdinalIgnoreCase),
        };

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
        // Relativity and Subtype must match
        if (add.Relativity != remove.Relativity || add.Subtype != remove.Subtype)
        {
            return false;
        }

        // Special subtype-based rules
        if (!string.IsNullOrEmpty(add.Subtype) && PairingRules.TryGetValue(add.Subtype, out var rule))
        {
            return rule(add, remove);
        }

        // Unit-based (CreateUnit)
        if (string.Equals(add.Type, CREATE_UNIT_TYPE, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(remove.Type, CREATE_UNIT_TYPE, StringComparison.OrdinalIgnoreCase))
        {
            return string.Equals(add.Unit, remove.Unit, StringComparison.OrdinalIgnoreCase);
        }

        // Default: target-based
        return HasMatchingTarget(add, remove);
    }

    private static bool HasMatchingTarget(Effect add, Effect remove)
    {
        return add.Targets.Any(t1 => remove.Targets.Any(t2 => t1.Value == t2.Value));
    }

    private void AddEffectToXml(XElement effectsElement, Effect effect)
    {
        var effectElem = new XElement("effect", new XAttribute("mergeMode", effect.MergeMode.ToString().ToLower()));

        // Only include amount if it's meaningful (non-zero)
        if (effect.Amount != 0)
        {
            effectElem.Add(new XAttribute("amount",
                effect.Amount.ToString("0.00", CultureInfo.InvariantCulture))); // rounded to 2 decimals
        }

        if (effect.Relativity != null && effect.Relativity != Relativity.NULL)
        {
            effectElem.Add(new XAttribute("relativity", StringExtensions.ToPascalCase(effect.Relativity.ToString())));
        }

        if (!string.IsNullOrEmpty(effect.Subtype))
        {
            effectElem.Add(new XAttribute("subtype", effect.Subtype));
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

        if (!string.IsNullOrEmpty(effect.UnitType))
        {
            effectElem.Add(new XAttribute("unittype", effect.UnitType));
        }

        if (!string.IsNullOrEmpty(effect.ArmorType))
        {
            effectElem.Add(new XAttribute("armortype", effect.ArmorType));
        }

        if (effect.IgnoreRally != null) // Export even if empty string
        {
            effectElem.Add(new XAttribute("ignorerally", effect.IgnoreRally));
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
            new XAttribute("speed", pattern.Speed.ToString("0.00", CultureInfo.InvariantCulture)),
            new XAttribute("radius", pattern.Radius.ToString("0.00", CultureInfo.InvariantCulture)),
            new XAttribute("quantity", pattern.Quantity.ToString("0.00", CultureInfo.InvariantCulture)),
            new XAttribute("minradius", pattern.MinRadius.ToString("0.00", CultureInfo.InvariantCulture)));

        var offsetElem = new XElement("offset",
            new XAttribute("x", pattern.OffsetX.ToString("0.00", CultureInfo.InvariantCulture)),
            new XAttribute("y", pattern.OffsetY.ToString("0.00", CultureInfo.InvariantCulture)),
            new XAttribute("z", pattern.OffsetZ.ToString("0.00", CultureInfo.InvariantCulture)));
        patternElem.Add(offsetElem);

        return patternElem;
    }
}
