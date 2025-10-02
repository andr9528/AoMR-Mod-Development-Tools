using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tools.Model;
using Tools.Model.Mod;

namespace Tools.Persistence.Configuration;

public class TargetConfiguration : IEntityTypeConfiguration<Target>
{
    public void Configure(EntityTypeBuilder<Target> builder)
    {
        builder.ToTable("Targets");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Type).IsRequired().HasMaxLength(50);

        builder.Property(t => t.Value).HasMaxLength(200);
    }
}
