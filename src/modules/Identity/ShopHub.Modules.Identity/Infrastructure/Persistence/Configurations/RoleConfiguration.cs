using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopHub.Modules.Identity.Domain.Entities;

namespace ShopHub.Modules.Identity.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("roles");

        builder.Property(r => r.Id).HasColumnName("id");
        builder.Property(r => r.Name).HasColumnName("name").HasMaxLength(256);
        builder.Property(r => r.NormalizedName).HasColumnName("normalized_name").HasMaxLength(256);
        builder.Property(r => r.ConcurrencyStamp).HasColumnName("concurrency_stamp");

        // Custom columns
        builder.Property(r => r.Description).HasColumnName("description").HasMaxLength(500);
        builder.Property(r => r.CreatedAt).HasColumnName("created_at").IsRequired();
    }
}