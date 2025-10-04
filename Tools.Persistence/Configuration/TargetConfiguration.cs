using System.Text.Json;
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

        builder.Property(e => e.ExtraAttributes).HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?) null),
            v => JsonSerializer.Deserialize<Dictionary<string, string?>>(v, (JsonSerializerOptions?) null) ??
                 new Dictionary<string, string?>());
    }
}
