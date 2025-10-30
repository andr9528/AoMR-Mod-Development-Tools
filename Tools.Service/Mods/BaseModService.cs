using System.Reflection;
using System.Xml.Linq;
using Tools.Abstraction.Interfaces;

namespace Tools.Service.Mods;

public abstract class BaseModService : IModService
{
    protected abstract string ModFolderName { get; }

    /// <inheritdoc />
    public XDocument? AdditionalTechTreeContent()
    {
        // Base folder where the app runs (bin output)
        string baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? AppContext.BaseDirectory;

        // Build full path: /Mods/<ModFolderName>/techtree_mods.xml
        string xmlPath = Path.Combine(baseDir, "Mods", ModFolderName, "techtree_mods.xml");

        return File.Exists(xmlPath) ? XDocument.Load(xmlPath) : null;
    }

    /// <inheritdoc />
    public XDocument? AdditionalProtoUnitContent()
    {
        // Base folder where the app runs (bin output)
        string baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? AppContext.BaseDirectory;

        // Build full path: /Mods/<ModFolderName>/proto_mods.xml
        string xmlPath = Path.Combine(baseDir, "Mods", ModFolderName, "proto_mods.xml");

        return File.Exists(xmlPath) ? XDocument.Load(xmlPath) : null;
    }
}
