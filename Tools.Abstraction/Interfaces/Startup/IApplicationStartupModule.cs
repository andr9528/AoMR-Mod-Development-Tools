namespace Tools.Abstraction.Interfaces.Startup;

public interface IApplicationStartupModule<TApplicationBuilder>
{
    void ConfigureApplication(TApplicationBuilder app);
    string Name { get; }
}
