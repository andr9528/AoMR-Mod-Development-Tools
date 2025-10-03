using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tools.Model.Mod;

namespace Tools.Persistence.Configuration;

public class PatternConfiguration : IEntityTypeConfiguration<Pattern>
{
    public void Configure(EntityTypeBuilder<Pattern> builder)
    {
        builder.ToTable("Patterns");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Type).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Value).HasMaxLength(200).IsRequired();
        builder.Property(p => p.Quantity).IsRequired();

        builder.HasOne(p => p.Effect).WithMany(e => e.Patterns).HasForeignKey(p => p.EffectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
