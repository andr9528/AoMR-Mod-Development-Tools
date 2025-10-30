using Tools.Uno.Abstraction;
using Tools.Uno.Extensions;
using Tools.Uno.Presentation.Factory;

namespace Tools.Uno.Presentation;

public sealed partial class ToolSelector : NavigationView
{
    private readonly IServiceProvider serviceProvider;
    private ListView menuList = null!;

    public ToolSelector(IServiceProvider sp, IEnumerable<IModRegion> regionDefinitions)
    {
        serviceProvider = sp;

        IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
        IsPaneToggleButtonVisible = false;
        IsSettingsVisible = false;

        PaneDisplayMode = NavigationViewPaneDisplayMode.LeftCompact;
        CompactPaneLength = 200;

        var modRegions = CreateMenuList(regionDefinitions);

        PaneCustomContent = menuList;

        // Default selection
        if (modRegions.Any())
        {
            menuList.SelectedIndex = 0;
        }
    }

    private List<IModRegion> CreateMenuList(IEnumerable<IModRegion> regionDefinitions)
    {
        var modRegions = regionDefinitions.ToList();
        menuList = new ListView
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32)),
            SelectionMode = ListViewSelectionMode.Single,
            ItemsSource = modRegions,
            ItemTemplate = CreateMenuItemTemplate(),
            ItemContainerStyle = CreateMenuItemStyle(),
            Margin = new Thickness(10),
        };
        menuList.SelectionChanged += MenuList_SelectionChanged;
        return modRegions;
    }

    private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (menuList.SelectedItem is IModRegion region)
        {
            DispatcherQueue.TryEnqueue(() => Content = region.CreateControl(serviceProvider));
        }
    }

    private DataTemplate CreateMenuItemTemplate()
    {
        return new DataTemplate(() =>
        {
            Grid grid = GridFactory.CreateDefaultGrid();
            grid.DefineColumns(GridUnitType.Auto, [30,]);
            grid.DefineColumns(sizes: [30,]);
            grid.Width = CompactPaneLength;

            var iconPresenter = new ContentPresenter()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            iconPresenter.SetBinding(ContentPresenter.ContentProperty, new Binding {Path = nameof(IModRegion.Icon),});

            var text = new TextBlock
            {
                Foreground = new SolidColorBrush(Colors.White),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(5, 0, 0, 0),
            };
            text.SetBinding(TextBlock.TextProperty, new Binding {Path = nameof(IModRegion.DisplayName),});

            grid.Children.Add(iconPresenter.SetColumn(0));
            grid.Children.Add(text.SetColumn(1));

            return grid;
        });
    }

    private Style CreateMenuItemStyle()
    {
        var style = new Style(typeof(ListViewItem));
        style.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
        style.Setters.Add(new Setter(BorderThicknessProperty, new Thickness(0)));
        style.Setters.Add(new Setter(PaddingProperty, new Thickness(10)));
        style.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch));

        // highlight selection
        style.Setters.Add(new Setter(ForegroundProperty, new SolidColorBrush(Colors.White)));

        return style;
    }
}
