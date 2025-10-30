using System.Reflection;
using System.Xml.Linq;
using Tools.Abstraction.Interfaces;

namespace Tools.Service.Mods;

public abstract class BaseModService : IModService
{
    /// <inheritdoc />
    public XDocument? AdditionalTechTreeContent()
    {
        // Determine the directory of the current source assembly
        string assemblyDir =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? AppContext.BaseDirectory;

        // Build the full path to your XML file
        string xmlPath = Path.Combine(assemblyDir, "techtree_mods.xml");

        // If the file doesn’t exist, just return null (nothing extra to append)
        return !File.Exists(xmlPath) ? null : XDocument.Load(xmlPath);
    }

    /// <inheritdoc />
    public XDocument? AdditionalProtoUnitContent()
    {
        // Determine the directory of the current source assembly
        string assemblyDir =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? AppContext.BaseDirectory;

        // Build the full path to your XML file
        string xmlPath = Path.Combine(assemblyDir, "proto_mods.xml");

        // If the file doesn’t exist, just return null (nothing extra to append)
        return !File.Exists(xmlPath) ? null : XDocument.Load(xmlPath);
    }
}
