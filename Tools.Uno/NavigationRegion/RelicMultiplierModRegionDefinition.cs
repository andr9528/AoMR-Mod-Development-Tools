using Tools.Uno.Abstraction;
using Tools.Uno.Presentation;
using Tools.Uno.Presentation.Region;

namespace Tools.Uno.NavigationRegion;

public class RelicMultiplierModRegionDefinition : IModRegion
{
    public string DisplayName => "Relic Multiplier Mod";

    public IconElement Icon => new SymbolIcon(Symbol.Favorite);

    public UIElement CreateControl(IServiceProvider services)
    {
        Console.WriteLine($"Changing tool to: {nameof(RelicMultiplierModRegion)}");
        // Use DI to build the region
        return ActivatorUtilities.CreateInstance<RelicMultiplierModRegion>(services);
    }
}
