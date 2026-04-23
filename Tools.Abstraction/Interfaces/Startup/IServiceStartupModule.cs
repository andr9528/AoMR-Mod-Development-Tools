using Microsoft.Extensions.DependencyInjection;

namespace Tools.Abstraction.Interfaces.Startup;

public interface IServiceStartupModule
{
    void ConfigureServices(IServiceCollection services);
    string Name { get; }
}
