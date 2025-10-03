using Tools.Uno.Abstraction;
using Tools.Uno.Presentation;
using Tools.Uno.Presentation.Region;

namespace Tools.Uno.NavigationRegion;

public class RelicModRegionDefinition : IModRegion
{
    public string DisplayName => "Relic Mod";

    public IconElement Icon => new SymbolIcon(Symbol.Favorite);

    public UIElement CreateControl(IServiceProvider services)
    {
        Console.WriteLine($"Changing tool to: {nameof(RelicModRegion)}");
        // Use DI to build the region
        return ActivatorUtilities.CreateInstance<RelicModRegion>(services);
    }
}
