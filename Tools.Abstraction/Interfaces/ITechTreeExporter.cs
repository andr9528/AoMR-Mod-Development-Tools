using System.Xml.Linq;

namespace Tools.Abstraction.Interfaces;

public interface ITechTreeExporter
{
    XDocument ExportToXml();
}
