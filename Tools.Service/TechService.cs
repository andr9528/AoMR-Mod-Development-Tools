using System.Xml.Linq;
using Tools.Abstraction.Interfaces;

namespace Tools.Service;

public class TechService
{
    private readonly IXmlLoader loader;
    private readonly IXmlExporter exporter;

    public TechService(IXmlLoader loader, IXmlExporter exporter)
    {
        this.loader = loader;
        this.exporter = exporter;
    }

    public async Task ImportTechTreeAsync(string path)
    {
        await loader.LoadFromFileAsync(path);
    }

    /// <summary>
    /// Generates and exports the tech tree XML file.
    /// </summary>
    /// <param name="inputFilePath"></param>
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
