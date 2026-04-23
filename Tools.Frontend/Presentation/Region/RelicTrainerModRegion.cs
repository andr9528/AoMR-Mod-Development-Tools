using Tools.Frontend.Presentation.Factory;
using Tools.Frontend.Presentation.Region.Logic;
using Tools.Frontend.Presentation.Region.UserInterface;
using Tools.Frontend.Presentation.Region.ViewModels;
using Tools.Service;
using Tools.Service.Mods.RelicTrainer;

namespace Tools.Frontend.Presentation.Region;

public class RelicTrainerModRegion : Border
{
    public RelicTrainerModRegion(
        RelicTrainerModService relicTrainerService, TechService techService, ProtoService protoService)
    {
        this.ConfigureDefaultBorder();

        DataContext = new RelicTrainerModRegionViewModel();

        var logic = new RelicTrainerModRegionLogic(relicTrainerService, techService, protoService,
            (RelicTrainerModRegionViewModel) DataContext);
        var ui = new RelicTrainerModRegionUserInterface(logic, (RelicTrainerModRegionViewModel) DataContext);

        Child = ui.CreateContentGrid();
    }
}
