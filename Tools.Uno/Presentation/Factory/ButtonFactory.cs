namespace Tools.Uno.Presentation.Factory;

public static class ButtonFactory
{
    public static Button CreateDefaultButton()
    {
        var button = new Button
        {
            Margin = new Thickness(10),
            Padding = new Thickness(10, 5, 10, 5),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            IsTabStop = false,
        };

        button.Style(Theme.Button.Styles.Filled);

        return button;
    }
}
