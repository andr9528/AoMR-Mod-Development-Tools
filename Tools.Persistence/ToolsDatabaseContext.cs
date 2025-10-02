using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Tools.Model;
using Tools.Model.Mod;
using Tools.Persistence.Configuration;

namespace Tools.Persistence;

public class ToolsDatabaseContext : DbContext
{
    public DbSet<Tech> Techs { get; set; } = null!;
    public DbSet<Effect> Effects { get; set; } = null!;
    public DbSet<Target> Targets { get; set; } = null!;

    public ToolsDatabaseContext(DbContextOptions<ToolsDatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToolsDatabaseContext).Assembly);
    }
}
