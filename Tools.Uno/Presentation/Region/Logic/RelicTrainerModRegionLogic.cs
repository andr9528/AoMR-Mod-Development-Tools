using Tools.Service;
using Tools.Service.Mods.RelicTrainer;

namespace Tools.Uno.Presentation.Region.Logic;

public class RelicTrainerModRegionLogic
{
    private readonly RelicTrainerModService relicTrainerService;
    private readonly TechService techService;
    private readonly ProtoService protoService;
    private readonly ViewModels.RelicTrainerModRegionViewModel viewModel;

    public RelicTrainerModRegionLogic(
        RelicTrainerModService relicTrainerService, TechService techService, ProtoService protoService,
        ViewModels.RelicTrainerModRegionViewModel viewModel)
    {
        this.relicTrainerService = relicTrainerService;
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

        try
        {
            viewModel.AppendStatus("Loading Tech Tree XML...");
            await techService.ImportTechTreeAsync(viewModel.InputFile);

            viewModel.AppendStatus("Creating Tech Tree File...");
            string techOutPath =
                techService.ExportTechTreeAsync(viewModel.InputFile, relicTrainerService.AdditionalTechTreeContent());
            viewModel.AppendStatus($"Saved Tech Tree file to {techOutPath}.");

            viewModel.AppendStatus("Creating Proto Units File...");
            string protoOutPath = protoService.ExportProtoUnitsAsync(viewModel.InputFile,
                relicTrainerService.AdditionalProtoUnitContent());
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
