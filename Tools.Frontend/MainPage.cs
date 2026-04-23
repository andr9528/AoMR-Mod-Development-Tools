using Tools.Frontend.Extensions;
using Tools.Frontend.Presentation;
using Tools.Frontend.Presentation.Factory;

namespace Tools.Frontend;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        Background = new SolidColorBrush(Colors.LightGray);

        Grid grid = GridFactory.CreateDefaultGrid();

        var selector = ActivatorUtilities.CreateInstance<ToolSelector>(App.Startup.ServiceProvider);

        grid.Children.Add(selector.SetRow(0).SetColumn(0));

        Content = grid;
    }
}
