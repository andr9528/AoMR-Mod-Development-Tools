using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tools.Model;
using Tools.Model.Mod;

namespace Tools.Persistence.Configuration;

public class EffectConfiguration : IEntityTypeConfiguration<Effect>
{
    public void Configure(EntityTypeBuilder<Effect> builder)
    {
        builder.ToTable("Effects");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Type).IsRequired().HasMaxLength(50);

        builder.Property(e => e.Action).HasMaxLength(100);

        builder.Property(e => e.Subtype).HasMaxLength(100);

        builder.Property(e => e.Resource).HasMaxLength(50);

        builder.Property(e => e.Unit).HasMaxLength(100);

        builder.Property(e => e.UnitType).HasMaxLength(100);

        builder.Property(e => e.Amount).HasColumnType("REAL"); // SQLite numeric

        builder.Property(e => e.MergeMode).HasConversion<string>() // store as string: "Remove", "Add"
            .HasMaxLength(10);

        builder.Property(e => e.Relativity).HasConversion<string>() // store as string: "Absolute", "BasePercent", etc.
            .HasMaxLength(20);

        builder.HasMany(e => e.Targets).WithOne(t => t.Effect).HasForeignKey(t => t.EffectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.ExtraAttributes).HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?) null),
            v => JsonSerializer.Deserialize<Dictionary<string, string?>>(v, (JsonSerializerOptions?) null) ??
                 new Dictionary<string, string?>());
    }
}
