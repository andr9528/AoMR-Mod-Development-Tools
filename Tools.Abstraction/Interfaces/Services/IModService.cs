using System.Xml.Linq;

namespace Tools.Abstraction.Interfaces.Services;

public interface IModService
{
    XDocument? AdditionalTechTreeContent();
    XDocument? AdditionalProtoUnitContent();
}
