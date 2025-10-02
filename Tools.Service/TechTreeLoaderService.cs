using System.Xml.Linq;
using Tools.Abstraction.Enum;
using Tools.Abstraction.Interfaces;
using Tools.Model;
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
                    Amount = double.TryParse((string) effectElem.Attribute("amount"), out double amt) ? amt : 0,
                    Subtype = (string) effectElem.Attribute("subtype") ?? string.Empty,
                    Resource = (string) effectElem.Attribute("resource"),
                    Unit = (string) effectElem.Attribute("unit"),
                    Generator = (string) effectElem.Attribute("generator"),
                    Relativity = ParseEnum<Relativity>((string) effectElem.Attribute("relativity") ?? "Absolute"),
                };

                foreach (XElement targetElem in effectElem.Descendants("target"))
                {
                    var target = new Target
                    {
                        Type = (string) targetElem.Attribute("type") ?? string.Empty,
                        Value = targetElem.Value?.Trim(),
                    };
                    effect.Targets.Add(target);
                }

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

    private static T ParseEnum<T>(string value) where T : struct
    {
        return Enum.TryParse<T>(value, true, out T result) ? result : default;
    }
}
