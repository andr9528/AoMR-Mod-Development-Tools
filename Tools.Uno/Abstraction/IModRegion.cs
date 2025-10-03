namespace Tools.Uno.Abstraction;

public interface IModRegion
{
    /// <summary>
    /// Display name shown in the NavigationView.
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// Icon for the NavigationView item (can be SymbolIcon, FontIcon, etc.).
    /// </summary>
    IconElement Icon { get; }

    /// <summary>
    /// Creates the region control (usually a Border wrapping the UI grid).
    /// </summary>
    UIElement CreateControl(IServiceProvider services);
}
