using System.Xml.Linq;
using Tools.Abstraction.Interfaces;

namespace Tools.Service.Xml;

public class ProtoExportService : IXmlExporter
{
    /// <inheritdoc />
    public XDocument ExportToXml(XDocument? additionalContent = null)
    {
        throw new NotImplementedException();
    }
}
