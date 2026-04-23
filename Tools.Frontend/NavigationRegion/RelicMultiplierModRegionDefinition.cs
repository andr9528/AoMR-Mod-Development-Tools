using Tools.Frontend.Abstraction;
using Tools.Frontend.Presentation.Region;

namespace Tools.Frontend.NavigationRegion;

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
