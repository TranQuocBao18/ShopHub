using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopHub.Modules.Identity.Domain.Entities;

namespace ShopHub.Modules.Identity.Infrastructure.Persistence.Configurations;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("roles", "identity");

        builder.Property(r => r.Id).HasColumnName("id");
        builder.Property(r => r.Name).HasColumnName("name").HasMaxLength(50);
        builder.Property(r => r.NormalizedName).HasColumnName("normalized_name").HasMaxLength(50);
        builder.Property(r => r.ConcurrencyStamp).HasColumnName("concurrency_stamp").IsConcurrencyToken();
        builder.Property(r => r.Description).HasColumnName("description").HasMaxLength(255);
        builder.Property(r => r.CreatedAt).HasColumnName("created_at");

        builder.HasIndex(r => r.NormalizedName).IsUnique();
    }
}
