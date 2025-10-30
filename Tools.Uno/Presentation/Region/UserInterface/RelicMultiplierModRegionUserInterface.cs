using Microsoft.UI.Xaml.Interop;
using Tools.Uno.Extensions;
using Tools.Uno.Presentation.Converter;
using Tools.Uno.Presentation.Core;
using Tools.Uno.Presentation.Factory;
using Tools.Uno.Presentation.Region.Logic;
using Tools.Uno.Presentation.Region.ViewModels;

namespace Tools.Uno.Presentation.Region.UserInterface;

public class RelicMultiplierModRegionUserInterface : BaseUserInterface
{
    private readonly RelicMultiplierModRegionLogic logic;
    private readonly RelicMultiplierModRegionViewModel viewModel;

    public RelicMultiplierModRegionUserInterface(
        RelicMultiplierModRegionLogic logic, RelicMultiplierModRegionViewModel viewModel)
    {
        this.logic = logic;
        this.viewModel = viewModel;
    }

    /// <inheritdoc />
    protected override void ConfigureContentGrid(Grid grid)
    {
        // Cut row heights down: 3 rows of 30 each, last row auto for status
        grid.DefineRows(GridUnitType.Auto, [20, 20, 20, 20,]);
        grid.DefineRows(GridUnitType.Star, [20,]);
        grid.DefineColumns(sizes: [100,]);
    }

    /// <inheritdoc />
    protected override void AddChildrenToGrid(Grid grid)
    {
        grid.Children.Add(CreateHeader().SetRow(0));
        grid.Children.Add(CreateFilePickerGroup().SetRow(1));
        grid.Children.Add(CreateMultiplierGroup().SetRow(2));
        grid.Children.Add(CreateRunButton().SetRow(3));
        grid.Children.Add(CreateStatusView().SetRow(4));

        //grid.ShowGridLines(Colors.DeepPink);
    }

    private TextBlock CreateHeader()
    {
        return new TextBlock()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            Foreground = new SolidColorBrush(Colors.Black),
            Text = "Relic Multiplier Mod Generator",
        };
    }

    private Grid CreateFilePickerGroup()
    {
        Grid grid = GridFactory.CreateDefaultGrid();

        Button button = ButtonFactory.CreateDefaultButton();
        button.Content = "Select techtree.xml";
        button.Click += async (_, _) => await logic.SelectFileAsync();

        grid.Children.Add(button);
        return grid;
    }

    private Grid CreateMultiplierGroup()
    {
        Grid grid = GridFactory.CreateDefaultGrid();
        grid.DefineRows(sizes: [30, 30,]); // label and slider, reduced height

        var label = new TextBlock
        {
            Foreground = new SolidColorBrush(Colors.Black),
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 10, 0),
        };

        // Bind "Multiplier: X" dynamically
        var binding = new Binding
        {
            Path = nameof(viewModel.Multiplier),
            Mode = BindingMode.OneWay,
            Converter = new MultiplierLabelConverter(),
        };
        label.SetBinding(TextBlock.TextProperty, binding);

        var slider = new Slider
        {
            Minimum = 1,
            Maximum = 50,
            TickFrequency = 1,
            StepFrequency = 1,
            Width = 200,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
        };
        slider.SetBinding(RangeBase.ValueProperty, new Binding
        {
            Path = nameof(viewModel.Multiplier),
            Mode = BindingMode.TwoWay,
        });

        grid.Children.Add(label.SetRow(0));
        grid.Children.Add(slider.SetRow(1));

        return grid;
    }

    private Grid CreateRunButton()
    {
        Grid grid = GridFactory.CreateDefaultGrid();

        Button button = ButtonFactory.CreateDefaultButton();
        button.Content = "Generate Mod";
        button.Click += async (_, _) => await logic.RunAsync();

        grid.Children.Add(button);
        return grid;
    }

    private ListView CreateStatusView()
    {
        var list = new ListView
        {
            IsItemClickEnabled = false,
            SelectionMode = ListViewSelectionMode.None,
            Margin = new Thickness(20, 20, 20, 8),
            Padding = new Thickness(5),
            ItemsPanel = new ItemsPanelTemplate(() => new StackPanel
            {
                Orientation = Orientation.Vertical,
            }),
            ItemTemplate = new DataTemplate(CreateStatusItemTextBlock),

            BorderBrush = new SolidColorBrush(Colors.Black),
            BorderThickness = new Thickness(1),
            VerticalAlignment = VerticalAlignment.Stretch,
        };

        var binding = new Binding()
        {
            Path = nameof(viewModel.StatusMessages),
            Mode = BindingMode.OneWay,
        };

        list.SetBinding(ItemsControl.ItemsSourceProperty, binding);

        return list;
    }

    private TextBlock CreateStatusItemTextBlock()
    {
        var textBlock = new TextBlock
        {
            Foreground = new SolidColorBrush(Colors.Black),
            TextWrapping = TextWrapping.WrapWholeWords,
        };

        textBlock.SetBinding(TextBlock.TextProperty, new Binding());

        return textBlock;
    }
}
