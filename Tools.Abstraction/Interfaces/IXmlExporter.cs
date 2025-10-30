using System.Xml.Linq;

namespace Tools.Abstraction.Interfaces;

public interface IXmlExporter
{
    XDocument ExportToXml(XDocument? additionalContent = null);
}
