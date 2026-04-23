using System.Xml.Linq;

namespace Tools.Abstraction.Interfaces.Services;

public interface IXmlExporter
{
    XDocument ExportToXml(XDocument? additionalContent = null);
}
