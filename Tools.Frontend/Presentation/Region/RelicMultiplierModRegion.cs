using Tools.Frontend.Presentation.Factory;
using Tools.Frontend.Presentation.Region.Logic;
using Tools.Frontend.Presentation.Region.UserInterface;
using Tools.Frontend.Presentation.Region.ViewModels;
using Tools.Service;
using Tools.Service.Mods.RelicMultiplier;

namespace Tools.Frontend.Presentation.Region;

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
