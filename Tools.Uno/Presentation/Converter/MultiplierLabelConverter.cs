namespace Tools.Uno.Presentation.Converter;

public sealed class MultiplierLabelConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value switch
        {
            double d => $"Multiplier: {Math.Round(d):0}",
            int i => $"Multiplier: {i}",
            var _ => "Multiplier: -",
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
