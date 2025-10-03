using Tools.Abstraction.Interfaces;
using Tools.Service;
using Tools.Uno.Presentation.Factory;
using Tools.Uno.Presentation.Region.Logic;
using Tools.Uno.Presentation.Region.UserInterface;

namespace Tools.Uno.Presentation.Region;

public class RelicModRegion : Border
{
    public RelicModRegion(RelicModService relicService, ITechTreeLoader loader)
    {
        this.ConfigureDefaultBorder();

        DataContext = new ViewModel.RelicModRegionViewModel();

        var logic = new RelicModRegionLogic(relicService, loader);
        var ui = new RelicModRegionUserInterface(logic, (ViewModel.RelicModRegionViewModel) DataContext);

        Child = ui.CreateContentGrid();
    }
}
