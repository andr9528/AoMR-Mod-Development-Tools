using Microsoft.EntityFrameworkCore;
using Tools.Persistence;
using Path = System.IO.Path;

namespace Tools.Uno.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddToolsDatabase(this IServiceCollection services)
    {
        string basePath = AppContext.BaseDirectory;
        string dbPath = Path.Combine(basePath, "Database.db");

        services.AddDbContext<ToolsDatabaseContext>(options => { options.UseSqlite($"Data Source={dbPath}"); });

        // Ensure database is re-created on startup
        using IServiceScope scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ToolsDatabaseContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return services;
    }
}
