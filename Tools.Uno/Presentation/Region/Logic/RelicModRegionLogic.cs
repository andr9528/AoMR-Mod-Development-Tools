using System.Xml.Linq;
using Tools.Abstraction.Interfaces;
using Tools.Service;
using Tools.Service.Mods.RelicMultiplier;
using Tools.Uno.Presentation.Region.ViewModels;
using Path = System.IO.Path;

namespace Tools.Uno.Presentation.Region.Logic;

public class RelicModRegionLogic
{
    private readonly RelicModService relicService;
    private readonly TechService techService;
    private readonly ProtoService protoService;

    public RelicModRegionLogic(RelicModService relicService, TechService techService, ProtoService protoService)
    {
        this.relicService = relicService;
        this.techService = techService;
        this.protoService = protoService;
    }

    public async Task SelectFileAsync(RelicModRegionViewModel viewModel)
    {
        var picker = new Windows.Storage.Pickers.FileOpenPicker();
        picker.FileTypeFilter.Add(".xml");

        WinRT.Interop.InitializeWithWindow.Initialize(picker,
            WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow));

        StorageFile? file = await picker.PickSingleFileAsync();
        if (file != null)
        {
            viewModel.InputFile = file.Path;
            viewModel.Status = $"File selected ({viewModel.InputFile}). Waiting...";
        }
    }

    public async Task RunAsync(RelicModRegionViewModel viewModel)
    {
        viewModel.Status = "Generating...";

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
            await techService.ImportTechTreeAsync(viewModel.InputFile);

            viewModel.Status = "Applying multiplier...";
            await relicService.ApplyMultiplierAsync(viewModel.Multiplier);

            viewModel.Status = "Creating Tech File";
            string techOutPath =
                techService.ExportTechTreeAsync(viewModel.InputFile, relicService.AdditionalTechTreeContent());

            viewModel.Status = $"Saved Tech file to {techOutPath}...";
            viewModel.Status = "Creating Proto File";
            string protoOutPath =
                protoService.ExportProtoUnitsAsync(viewModel.InputFile, relicService.AdditionalProtoUnitContent());

            viewModel.Status = $"Saved Proto file to {protoOutPath}. Done.";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            viewModel.Status = $"Error: {ex.Message}";
        }
    }
}
