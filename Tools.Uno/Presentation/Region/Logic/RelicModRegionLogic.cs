using System.Xml.Linq;
using Tools.Abstraction.Interfaces;
using Tools.Service;
using Path = System.IO.Path;

namespace Tools.Uno.Presentation.Region.Logic;

public class RelicModRegionLogic
{
    private readonly RelicModService relicService;
    private readonly ITechTreeLoader loader;

    public RelicModRegionLogic(RelicModService relicService, ITechTreeLoader loader)
    {
        this.relicService = relicService;
        this.loader = loader;
    }

    public async Task SelectFileAsync(ViewModel.RelicModRegionViewModel vm)
    {
        var picker = new Windows.Storage.Pickers.FileOpenPicker();
        picker.FileTypeFilter.Add(".xml");

        WinRT.Interop.InitializeWithWindow.Initialize(picker,
            WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow));

        StorageFile? file = await picker.PickSingleFileAsync();
        if (file != null)
        {
            vm.InputFile = file.Path;
        }
    }

    public async Task RunAsync(ViewModel.RelicModRegionViewModel viewModel)
    {
        if (string.IsNullOrEmpty(viewModel.InputFile))
        {
            viewModel.Status = "Please select a techtree.xml file first.";
            return;
        }

        if (viewModel.Multiplier <= 0)
        {
            viewModel.Status = "Multiplier must be positive.";
            return;
        }

        try
        {
            viewModel.Status = "Loading XML...";
            await loader.LoadFromFileAsync(viewModel.InputFile);

            viewModel.Status = "Applying multiplier...";
            await relicService.ApplyMultiplierAsync(viewModel.Multiplier);

            XDocument output = relicService.ExportToXml();
            string outPath = Path.Combine(Path.GetDirectoryName((string?) viewModel.InputFile)!, "techtree_mods.xml");
            output.Save(outPath);

            viewModel.Status = $"Done! Saved to {outPath}";
        }
        catch (Exception ex)
        {
            viewModel.Status = $"Error: {ex.Message}";
        }
    }
}
