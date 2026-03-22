using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopHub.Modules.Identity.Infrastructure.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.ToTable("user_roles");
        builder.Property(ur => ur.UserId).HasColumnName("user_id");
        builder.Property(ur => ur.RoleId).HasColumnName("role_id");
    }
}

public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
    {
        builder.ToTable("user_claims");
        builder.Property(uc => uc.Id).HasColumnName("id");
        builder.Property(uc => uc.UserId).HasColumnName("user_id");
        builder.Property(uc => uc.ClaimType).HasColumnName("claim_type");
        builder.Property(uc => uc.ClaimValue).HasColumnName("claim_value");
    }
}

public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
    {
        builder.ToTable("user_logins");
        builder.Property(ul => ul.LoginProvider).HasColumnName("login_provider");
        builder.Property(ul => ul.ProviderKey).HasColumnName("provider_key");
        builder.Property(ul => ul.ProviderDisplayName).HasColumnName("provider_display_name");
        builder.Property(ul => ul.UserId).HasColumnName("user_id");
    }
}

public class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
    {
        builder.ToTable("user_tokens");
        builder.Property(ut => ut.UserId).HasColumnName("user_id");
        builder.Property(ut => ut.LoginProvider).HasColumnName("login_provider");
        builder.Property(ut => ut.Name).HasColumnName("name");
        builder.Property(ut => ut.Value).HasColumnName("value");
    }
}

public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
    {
        builder.ToTable("role_claims");
        builder.Property(rc => rc.Id).HasColumnName("id");
        builder.Property(rc => rc.RoleId).HasColumnName("role_id");
        builder.Property(rc => rc.ClaimType).HasColumnName("claim_type");
        builder.Property(rc => rc.ClaimValue).HasColumnName("claim_value");
    }
}