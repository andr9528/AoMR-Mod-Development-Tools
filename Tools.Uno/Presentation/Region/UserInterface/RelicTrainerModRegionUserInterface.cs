using Tools.Uno.Extensions;
using Tools.Uno.Presentation.Core;
using Tools.Uno.Presentation.Factory;
using Tools.Uno.Presentation.Region.Logic;

namespace Tools.Uno.Presentation.Region.UserInterface;

public class RelicTrainerModRegionUserInterface : BaseUserInterface
{
    private readonly RelicTrainerModRegionLogic logic;
    private readonly ViewModels.RelicTrainerModRegionViewModel viewModel;

    public RelicTrainerModRegionUserInterface(
        RelicTrainerModRegionLogic logic, ViewModels.RelicTrainerModRegionViewModel viewModel)
    {
        this.logic = logic;
        this.viewModel = viewModel;
    }

    /// <inheritdoc />
    protected override void ConfigureContentGrid(Grid grid)
    {
        // Cut row heights down: 3 rows of 30 each, last row auto for status
        grid.DefineRows(GridUnitType.Auto, [20, 20, 20,]);
        grid.DefineRows(GridUnitType.Star, [20,]);
        grid.DefineColumns(sizes: [100,]);
    }

    /// <inheritdoc />
    protected override void AddChildrenToGrid(Grid grid)
    {
        grid.Children.Add(CreateHeader().SetRow(0));
        grid.Children.Add(CreateFilePickerGroup().SetRow(1));
        grid.Children.Add(CreateRunButton().SetRow(2));
        grid.Children.Add(CreateStatusView().SetRow(3));

        //grid.ShowGridLines(Colors.DeepPink);
    }

    private TextBlock CreateHeader()
    {
        return new TextBlock()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            Foreground = new SolidColorBrush(Colors.Black),
            Text = "Relic Trainer Mod Generator",
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
