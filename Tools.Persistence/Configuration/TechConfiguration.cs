using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tools.Model;
using Tools.Model.Mod;

namespace Tools.Persistence.Configuration;

public class TechConfiguration : IEntityTypeConfiguration<Tech>
{
    public void Configure(EntityTypeBuilder<Tech> builder)
    {
        builder.ToTable("Techs");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name).IsRequired().HasMaxLength(200);

        builder.Property(t => t.Type).HasMaxLength(50);

        builder.HasMany(t => t.Effects).WithOne(e => e.Tech).HasForeignKey(e => e.TechId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
