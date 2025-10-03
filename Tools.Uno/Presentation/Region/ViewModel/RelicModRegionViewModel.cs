namespace Tools.Uno.Presentation.Region.ViewModel;

public partial class RelicModRegionViewModel : ObservableObject
{
    [ObservableProperty] private string status = "Idle...";
    [ObservableProperty] private string? inputFile;
    [ObservableProperty] private int multiplier = 5;
}
