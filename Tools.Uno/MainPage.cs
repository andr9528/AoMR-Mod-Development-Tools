using Tools.Uno.Abstraction;
using Tools.Uno.Extensions;
using Tools.Uno.NavigationRegion;
using Tools.Uno.Presentation;
using Tools.Uno.Presentation.Factory;

namespace Tools.Uno;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        Background = new SolidColorBrush(Colors.LightGray);

        Grid grid = GridFactory.CreateDefaultGrid();

        var selector = ActivatorUtilities.CreateInstance<ToolSelector>(App.ServiceProvider);

        grid.Children.Add(selector.SetRow(0).SetColumn(0));

        Content = grid;
    }
}
