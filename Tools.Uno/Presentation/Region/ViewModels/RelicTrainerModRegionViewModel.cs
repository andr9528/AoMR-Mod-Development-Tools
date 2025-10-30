using System.Collections.ObjectModel;

namespace Tools.Uno.Presentation.Region.ViewModels;

public partial class RelicTrainerModRegionViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<string> statusMessages = new();
    [ObservableProperty] private string? inputFile;

    public void AppendStatus(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        StatusMessages.Add(message);
    }

    public RelicTrainerModRegionViewModel()
    {
        AppendStatus("Idling...");
    }
}
