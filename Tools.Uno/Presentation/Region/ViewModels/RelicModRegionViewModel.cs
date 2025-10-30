using System.Collections.ObjectModel;

namespace Tools.Uno.Presentation.Region.ViewModels;

public partial class RelicModRegionViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<string> statusMessages = new();
    [ObservableProperty] private string? inputFile;
    [ObservableProperty] private int multiplier = 5;

    public void AppendStatus(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        StatusMessages.Add(message);
    }

    public RelicModRegionViewModel()
    {
        AppendStatus("Idling...");
    }
}
