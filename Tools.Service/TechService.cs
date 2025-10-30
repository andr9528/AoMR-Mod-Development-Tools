using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using Tools.Abstraction.Enum;
using Tools.Abstraction.Interfaces;

namespace Tools.Service;

public class TechService
{
    private readonly IXmlLoader loader;
    private readonly IXmlExporter exporter;

    public TechService(IServiceProvider sp)
    {
        loader = sp.GetRequiredKeyedService<IXmlLoader>(XmlKind.TECH);
        exporter = sp.GetRequiredKeyedService<IXmlExporter>(XmlKind.TECH);
    }

    /// <summary>
    /// Imports a technology tree from the specified file asynchronously.
    /// </summary>
    /// <param name="path">The file path from which to load the technology tree. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous import operation.</returns>
    public async Task ImportTechTreeAsync(string path)
    {
        await loader.LoadFromFileAsync(path);
    }

    /// <summary>
    /// Exports the technology tree to an XML file.
    /// </summary>
    /// <param name="inputFilePath">
    /// The input file path used to determine the output directory.
    /// </param>
    /// <param name="additionalContent">
    /// Optional additional XML content to include in the export.
    /// </param>
    /// <returns>
    /// Returns the output file path.
    /// </returns>
    public string ExportTechTreeAsync(string inputFilePath, XDocument? additionalContent = null)
    {
        XDocument xmlContent = exporter.ExportToXml(additionalContent);

        string outPath = Path.Combine(Path.GetDirectoryName((string?) inputFilePath)!, "techtree_mods.xml");
        xmlContent.Save(outPath);

        return outPath;
    }
}
