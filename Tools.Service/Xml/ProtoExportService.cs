using System.Xml.Linq;
using Tools.Abstraction.Interfaces;

namespace Tools.Service.Xml;

public class ProtoExportService : IXmlExporter
{
    /// <inheritdoc />
    public XDocument ExportToXml(XDocument? additionalContent = null)
    {
        var root = new XElement("protomods");

        AppendAdditionalContent(root, additionalContent);

        return new XDocument(root);
    }

    private void AppendAdditionalContent(XElement root, XDocument? additionalContent)
    {
        if (additionalContent?.Root is null)
        {
            return;
        }

        // Root must match (including namespace if any)
        if (additionalContent.Root.Name != root.Name)
        {
            Console.WriteLine(
                $"Additional Content root mismatch. Expected '{root.Name}', but was '{additionalContent.Root.Name}'.");
            return;
        }

        // Append *only the children* of the root (keeps a single root in the output)
        root.Add(additionalContent.Root.Elements());
    }
}
