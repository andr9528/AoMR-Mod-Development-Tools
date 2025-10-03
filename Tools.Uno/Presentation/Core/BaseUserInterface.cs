using Tools.Uno.Presentation.Factory;

namespace Tools.Uno.Presentation.Core;

public abstract class BaseUserInterface
{
    public Grid CreateContentGrid()
    {
        Grid grid = GridFactory.CreateDefaultGrid();

        ConfigureContentGrid(grid);
        AddChildrenToGrid(grid);

        return grid;
    }

    protected abstract void ConfigureContentGrid(Grid grid);
    protected abstract void AddChildrenToGrid(Grid grid);
}
