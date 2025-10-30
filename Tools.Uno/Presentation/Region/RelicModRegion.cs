using Tools.Abstraction.Interfaces;
using Tools.Service;
using Tools.Uno.Presentation.Factory;
using Tools.Uno.Presentation.Region.Logic;
using Tools.Uno.Presentation.Region.UserInterface;
using Tools.Uno.Presentation.Region.ViewModels;

namespace Tools.Uno.Presentation.Region;

public class RelicModRegion : Border
{
    public RelicModRegion(RelicModService relicService, IXmlLoader loader, IXmlExporter exporter)
    {
        this.ConfigureDefaultBorder();

        DataContext = new RelicModRegionViewModel();

        var logic = new RelicModRegionLogic(relicService, loader, exporter);
        var ui = new RelicModRegionUserInterface(logic, (RelicModRegionViewModel) DataContext);

        Child = ui.CreateContentGrid();
    }
}
