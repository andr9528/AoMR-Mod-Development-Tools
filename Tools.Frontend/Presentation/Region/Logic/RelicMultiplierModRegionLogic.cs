using Tools.Frontend.Presentation.Region.ViewModels;
using Tools.Service;
using Tools.Service.Mods.RelicMultiplier;

namespace Tools.Frontend.Presentation.Region.Logic;

public class RelicMultiplierModRegionLogic
{
    private readonly RelicMultiplierModService relicMultiplierService;
    private readonly TechService techService;
    private readonly ProtoService protoService;
    private readonly RelicMultiplierModRegionViewModel viewModel;
    private readonly ILogger<RelicMultiplierModRegionLogic> logger;

    public RelicMultiplierModRegionLogic(
        RelicMultiplierModService relicMultiplierService, TechService techService, ProtoService protoService,
        RelicMultiplierModRegionViewModel viewModel, ILoggerFactory loggerFactory)
    {
        this.relicMultiplierService = relicMultiplierService;
        this.techService = techService;
        this.protoService = protoService;
        this.viewModel = viewModel;
        logger = loggerFactory.CreateLogger<RelicMultiplierModRegionLogic>();
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
            logger.LogInformation("File Selected: {File}", viewModel.InputFile);
            viewModel.AppendStatus($"File selected: '{viewModel.InputFile}'.");
            viewModel.AppendStatus($"Waiting....");
        }
    }

    public async Task RunAsync()
    {
        viewModel.AppendStatus("Generating File...");
        logger.LogInformation("Generating File...");

        if (string.IsNullOrEmpty(viewModel.InputFile))
        {
            viewModel.AppendStatus("Please select a techtree.xml file first.");
            logger.LogWarning("No File was Selected, cancelling.");

            return;
        }

        if (viewModel.Multiplier <= 0)
        {
            viewModel.AppendStatus("Multiplier must be positive.");
            logger.LogWarning("Invalid Multiplier selected '{Multiplier}', cancelling.", viewModel.Multiplier);
            return;
        }

        try
        {
            viewModel.AppendStatus("Loading Tech Tree XML...");
            logger.LogInformation("Loading Tech Tree xml to Database from '{File}'...", viewModel.InputFile);
            await techService.ImportTechTreeAsync(viewModel.InputFile);

            viewModel.AppendStatus("Applying multiplier...");
            logger.LogInformation("Applying selected multiplier '{Multiplier}'...", viewModel.Multiplier);
            await relicMultiplierService.ApplyMultiplierAsync(viewModel.Multiplier);

            viewModel.AppendStatus("Creating Tech Tree File...");
            logger.LogInformation("Exporting 'techtree_mods.xml' file to beside input...");
            string techOutPath = techService.ExportTechTreeAsync(viewModel.InputFile,
                relicMultiplierService.AdditionalTechTreeContent());
            viewModel.AppendStatus($"Saved Tech Tree file to {techOutPath}.");

            viewModel.AppendStatus("Creating Proto Units File...");
            logger.LogInformation("Exporting 'proto_mods.xml' file to beside input...");
            string protoOutPath = protoService.ExportProtoUnitsAsync(viewModel.InputFile,
                relicMultiplierService.AdditionalProtoUnitContent());
            viewModel.AppendStatus($"Saved Proto Units file to {protoOutPath}.");

            logger.LogInformation("Completed creation of Multiplier mod files with succes.");
            viewModel.AppendStatus($"Done.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during attempt to generate multiplier mod files.");
            viewModel.AppendStatus($"Error: {ex.Message}");
        }
    }
}
