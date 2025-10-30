using System.Xml.Linq;
using Tools.Abstraction.Interfaces;
using Tools.Service;
using Tools.Service.Mods.RelicMultiplier;
using Tools.Uno.Presentation.Region.ViewModels;
using Path = System.IO.Path;

namespace Tools.Uno.Presentation.Region.Logic;

public class RelicMultiplierModRegionLogic
{
    private readonly RelicMultiplierModService relicMultiplierService;
    private readonly TechService techService;
    private readonly ProtoService protoService;
    private readonly RelicMultiplierModRegionViewModel viewModel;

    public RelicMultiplierModRegionLogic(
        RelicMultiplierModService relicMultiplierService, TechService techService, ProtoService protoService,
        RelicMultiplierModRegionViewModel viewModel)
    {
        this.relicMultiplierService = relicMultiplierService;
        this.techService = techService;
        this.protoService = protoService;
        this.viewModel = viewModel;
    }

    public async Task SelectFileAsync()
    {
        var picker = new Windows.Storage.Pickers.FileOpenPicker();
        picker.FileTypeFilter.Add(".xml");

        WinRT.Interop.InitializeWithWindow.Initialize(picker,
            WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow));

        StorageFile? file = await picker.PickSingleFileAsync();
        if (file != null)
        {
            viewModel.InputFile = file.Path;
            viewModel.AppendStatus($"File selected - {viewModel.InputFile}.");
            viewModel.AppendStatus($"Waiting....");
        }
    }

    public async Task RunAsync()
    {
        viewModel.AppendStatus("Generating File...");

        if (string.IsNullOrEmpty(viewModel.InputFile))
        {
            viewModel.AppendStatus("Please select a techtree.xml file first.");
            return;
        }

        if (viewModel.Multiplier <= 0)
        {
            viewModel.AppendStatus("Multiplier must be positive.");
            return;
        }

        try
        {
            viewModel.AppendStatus("Loading Tech Tree XML...");
            await techService.ImportTechTreeAsync(viewModel.InputFile);

            viewModel.AppendStatus("Applying multiplier...");
            await relicMultiplierService.ApplyMultiplierAsync(viewModel.Multiplier);

            viewModel.AppendStatus("Creating Tech Tree File...");
            string techOutPath = techService.ExportTechTreeAsync(viewModel.InputFile,
                relicMultiplierService.AdditionalTechTreeContent());
            viewModel.AppendStatus($"Saved Tech Tree file to {techOutPath}.");

            viewModel.AppendStatus("Creating Proto Units File...");
            string protoOutPath = protoService.ExportProtoUnitsAsync(viewModel.InputFile,
                relicMultiplierService.AdditionalProtoUnitContent());
            viewModel.AppendStatus($"Saved Proto Units file to {protoOutPath}.");

            viewModel.AppendStatus($"Done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            viewModel.AppendStatus($"Error: {ex.Message}");
        }
    }
}
