using Tools.Uno.Abstraction;
using Tools.Uno.Presentation;
using Tools.Uno.Presentation.Region;

namespace Tools.Uno.NavigationRegion;

public class RelicTrainerModRegionDefinition : IModRegion
{
    public string DisplayName => "Relic Trainer Mod";

    public IconElement Icon => new SymbolIcon(Symbol.Up);

    public UIElement CreateControl(IServiceProvider services)
    {
        Console.WriteLine($"Changing tool to: {nameof(RelicTrainerModRegionDefinition)}");
        // Use DI to build the region
        return ActivatorUtilities.CreateInstance<RelicTrainerModRegion>(services);
    }
}
