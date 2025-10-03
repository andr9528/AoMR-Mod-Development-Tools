using Tools.Abstraction.Enum;

namespace Tools.Model.Mod;

public class Effect
{
    public int Id { get; set; }

    public MergeMode MergeMode { get; set; }
    public string Type { get; set; } = string.Empty; // "Data", "CreateUnit", etc.
    public string? Action { get; set; }
    public double Amount { get; set; }
    public string Subtype { get; set; } = string.Empty;
    public string? Resource { get; set; }
    public string? Unit { get; set; }
    public string? Generator { get; set; }
    public Relativity Relativity { get; set; }

    public List<Target> Targets { get; set; } = new();
    public List<Pattern> Patterns { get; set; } = new();


    public int TechId { get; set; }
    public Tech Tech { get; set; } = null!;
}
