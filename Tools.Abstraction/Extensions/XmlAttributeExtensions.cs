using Tools.Abstraction.Enum;

namespace Tools.Abstraction.Extensions;

public static class XmlAttributeExtensions
{
    public static string ToXmlName(this EffectAttribute attr)
    {
        return attr switch
        {
            EffectAttribute.MERGE_MODE => "mergeMode",
            EffectAttribute.TYPE => "type",
            EffectAttribute.ACTION => "action",
            EffectAttribute.AMOUNT => "amount",
            EffectAttribute.SUBTYPE => "subtype",
            EffectAttribute.RESOURCE => "resource",
            EffectAttribute.UNIT => "unit",
            EffectAttribute.UNIT_TYPE => "unittype",
            EffectAttribute.RELATIVITY => "relativity",
            var _ => throw new ArgumentOutOfRangeException(nameof(attr), attr, "Unknown EffectAttribute"),
        };
    }

    public static bool Matches(this EffectAttribute attr, string xmlName)
    {
        return string.Equals(attr.ToXmlName(), xmlName, StringComparison.OrdinalIgnoreCase);
    }

    public static string ToXmlName(this PatternAttribute attr)
    {
        return attr switch
        {
            PatternAttribute.TYPE => "type",
            PatternAttribute.QUANTITY => "quantity",
            PatternAttribute.VALUE => "value",
            var _ => throw new ArgumentOutOfRangeException(nameof(attr), attr, "Unknown EffectAttribute"),
        };
    }

    public static bool Matches(this PatternAttribute attr, string xmlName)
    {
        return string.Equals(attr.ToXmlName(), xmlName, StringComparison.OrdinalIgnoreCase);
    }

    public static string ToXmlName(this TargetAttribute attr)
    {
        return attr switch
        {
            TargetAttribute.VALUE => "value",
            TargetAttribute.TYPE => "type",
            var _ => throw new ArgumentOutOfRangeException(nameof(attr), attr, "Unknown EffectAttribute"),
        };
    }

    public static bool Matches(this TargetAttribute attr, string xmlName)
    {
        return string.Equals(attr.ToXmlName(), xmlName, StringComparison.OrdinalIgnoreCase);
    }
}
