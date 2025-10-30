using Tools.Abstraction.Interfaces;
using Tools.Service;
using Tools.Service.Mods.RelicMultiplier;
using Tools.Service.Mods.RelicTrainer;
using Tools.Uno.Presentation.Factory;
using Tools.Uno.Presentation.Region.Logic;
using Tools.Uno.Presentation.Region.UserInterface;
using Tools.Uno.Presentation.Region.ViewModels;

namespace Tools.Uno.Presentation.Region;

public class RelicTrainerModRegion : Border
{
    public RelicTrainerModRegion(
        RelicTrainerModService relicTrainerService, TechService techService, ProtoService protoService)
    {
        this.ConfigureDefaultBorder();

        DataContext = new ViewModels.RelicTrainerModRegionViewModel();

        var logic = new RelicTrainerModRegionLogic(relicTrainerService, techService, protoService,
            (ViewModels.RelicTrainerModRegionViewModel) DataContext);
        var ui = new RelicTrainerModRegionUserInterface(logic, (ViewModels.RelicTrainerModRegionViewModel) DataContext);

        Child = ui.CreateContentGrid();
    }
}
