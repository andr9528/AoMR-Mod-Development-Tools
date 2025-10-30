using Tools.Abstraction.Interfaces;
using Tools.Service;
using Tools.Service.Mods.RelicMultiplier;
using Tools.Uno.Presentation.Factory;
using Tools.Uno.Presentation.Region.Logic;
using Tools.Uno.Presentation.Region.UserInterface;
using Tools.Uno.Presentation.Region.ViewModels;

namespace Tools.Uno.Presentation.Region;

public class RelicMultiplierModRegion : Border
{
    public RelicMultiplierModRegion(
        RelicMultiplierModService relicMultiplierService, TechService techService, ProtoService protoService)
    {
        this.ConfigureDefaultBorder();

        DataContext = new RelicMultiplierModRegionViewModel();

        var logic = new RelicMultiplierModRegionLogic(relicMultiplierService, techService, protoService,
            (RelicMultiplierModRegionViewModel) DataContext);
        var ui = new RelicMultiplierModRegionUserInterface(logic, (RelicMultiplierModRegionViewModel) DataContext);

        Child = ui.CreateContentGrid();
    }
}
