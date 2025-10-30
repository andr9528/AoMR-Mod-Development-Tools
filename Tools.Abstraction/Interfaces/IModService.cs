using System.Xml.Linq;

namespace Tools.Abstraction.Interfaces;

public interface IModService
{
    XDocument? AdditionalTechTreeContent();
    XDocument? AdditionalProtoUnitContent();
}
