using Microsoft.Extensions.Configuration;
using Tools.Abstraction.Enum;
using Tools.Abstraction.Interfaces.Services;
using Tools.Frontend.Abstraction;
using Tools.Frontend.Extensions;
using Tools.Frontend.NavigationRegion;
using Tools.Model.Uno;
using Tools.Service;
using Tools.Service.Mods.RelicMultiplier;
using Tools.Service.Mods.RelicTrainer;
using Tools.Service.Xml;
using Tools.Startup;
using Tools.Startup.Modules;

namespace Tools.Frontend;

public class UnoStartup : ModularStartup<IApplicationBuilder>
{
    private readonly IConfiguration configuration;
    private readonly ConfigurationService configurationService;

    public UnoStartup()
    {
        configurationService = new ConfigurationService();
        configuration = configurationService.BuildConfiguration();

        AddModule(new LoggingStartupModule(new[]
        {
            LogTarget.CONSOLE,
            LogTarget.FILE,
        }, configurationService.GetApplicationDataPath()));
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddToolsDatabase();
        services.AddKeyedScoped<IXmlLoader, TechTreeLoaderService>(XmlKind.TECH);
        services.AddKeyedScoped<IXmlExporter, TechTreeExportService>(XmlKind.TECH);
        services.AddKeyedScoped<IXmlExporter, ProtoExportService>(XmlKind.PROTO);

        services.AddScoped<RelicMultiplierModService>();
        services.AddScoped<RelicTrainerModService>();

        services.AddScoped<TechService>();
        services.AddScoped<ProtoService>();

        services.AddSingleton<IModRegion, RelicMultiplierModRegionDefinition>();
        services.AddSingleton<IModRegion, RelicTrainerModRegionDefinition>();

        // Later you can add more tools the same way:
        //services.AddSingleton<IModRegion, OtherToolRegionDefinition>();
        //services.AddSingleton<IModRegion, ThirdToolRegionDefinition>();
    }

    /// <inheritdoc />
    protected override void ConfigureApplication(IApplicationBuilder app)
    {
        base.ConfigureApplication(app);

#if DEBUG
        app.Configure(host => host.UseEnvironment(Environments.Development));
#endif

        app.Configure(host => host.UseConfiguration(configure: ConfigureConfigurationSource));
    }

    private IHostBuilder ConfigureConfigurationSource(IConfigBuilder configBuilder)
    {
        return configBuilder.EmbeddedSource<App>().Section<AppConfig>();
    }
}
