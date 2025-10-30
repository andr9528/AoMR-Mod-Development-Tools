using System.Xml.Linq;
using Tools.Abstraction.Interfaces;

namespace Tools.Service;

public class ProtoService
{
    private readonly IXmlExporter exporter;

    public ProtoService(IXmlExporter exporter)
    {
        this.exporter = exporter;
    }

    /// <summary>
    /// Exports the proto units to an XML file.
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
    public string ExportProtoUnitsAsync(string inputFilePath, XDocument? additionalContent = null)
    {
        XDocument xmlContent = exporter.ExportToXml(additionalContent);

        string outPath = Path.Combine(Path.GetDirectoryName((string?) inputFilePath)!, "proto_mods.xml");
        xmlContent.Save(outPath);

        return outPath;
    }
}
