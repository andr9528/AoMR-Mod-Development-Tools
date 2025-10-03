using Tools.Uno.Extensions;
using Tools.Uno.Presentation.Converter;
using Tools.Uno.Presentation.Core;
using Tools.Uno.Presentation.Factory;
using Tools.Uno.Presentation.Region.Logic;
using Tools.Uno.Presentation.Region.ViewModel;

namespace Tools.Uno.Presentation.Region.UserInterface;

public class RelicModRegionUserInterface : BaseUserInterface
{
    private readonly RelicModRegionLogic logic;
    private readonly RelicModRegionViewModel viewModel;

    public RelicModRegionUserInterface(RelicModRegionLogic logic, RelicModRegionViewModel viewModel)
    {
        this.logic = logic;
        this.viewModel = viewModel;
    }

    /// <inheritdoc />
    protected override void ConfigureContentGrid(Grid grid)
    {
        // Cut row heights down: 3 rows of 30 each, last row auto for status
        grid.DefineRows(GridUnitType.Auto, [20, 20, 20,]);
        grid.DefineRows(sizes: [20,]);
        grid.DefineColumns(sizes: [100,]);
    }

    /// <inheritdoc />
    protected override void AddChildrenToGrid(Grid grid)
    {
        grid.Children.Add(CreateFilePickerGroup().SetRow(0));
        grid.Children.Add(CreateMultiplierGroup().SetRow(1));
        grid.Children.Add(CreateRunButton().SetRow(2));
        grid.Children.Add(CreateStatusText().SetRow(3));
    }

    private Grid CreateFilePickerGroup()
    {
        Grid grid = GridFactory.CreateDefaultGrid();

        Button button = ButtonFactory.CreateDefaultButton();
        button.Content = "Select techtree.xml";
        button.Click += async (_, _) => await logic.SelectFileAsync(viewModel);

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
        button.Content = "Generate Relic Mod";
        button.Click += async (_, _) => await logic.RunAsync(viewModel);

        grid.Children.Add(button);
        return grid;
    }

    private TextBlock CreateStatusText()
    {
        var textBlock = new TextBlock
        {
            Foreground = new SolidColorBrush(Colors.Black),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };

        textBlock.SetBinding(TextBlock.TextProperty, new Binding
        {
            Path = nameof(viewModel.Status),
            Mode = BindingMode.OneWay,
        });

        return textBlock;
    }
}
