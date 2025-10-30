using Tools.Uno.Extensions;

namespace Tools.Uno.Presentation.Factory;

public static class GridFactory
{
    public static Grid CreateDefaultGrid()
    {
        return new Grid()
        {
            Margin = new Thickness(2),
            IsTabStop = false,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
        };
    }

    public static Grid CreateLeftAlignedGrid()
    {
        Grid grid = CreateDefaultGrid();

        grid.HorizontalAlignment = HorizontalAlignment.Left;

        return grid;
    }

    public static void ShowGridLines(this Grid grid, Color color)
    {
        int rows = grid.RowDefinitions.Count;
        int cols = grid.ColumnDefinitions.Count;

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                var border = new Border
                {
                    BorderBrush = new SolidColorBrush(color),
                    BorderThickness = new Thickness(1),
                };
                grid.Children.Add(border.SetRow(r).SetColumn(c));
            }
        }
    }
}
