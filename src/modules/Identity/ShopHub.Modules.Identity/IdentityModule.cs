using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShopHub.Modules.Identity.Domain.Entities;
using ShopHub.Modules.Identity.Domain.Repositories;
using ShopHub.Modules.Identity.Infrastructure.Persistence;
using ShopHub.Modules.Identity.Infrastructure.Services;
using ShopHub.Modules.Identity.Presentation.Controllers;
using ShopHub.SharedKernel;
using System.Reflection;
using System.Text;

namespace ShopHub.Modules.Identity;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("shophub")));

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<IdentityDbContext>()
        .AddDefaultTokenProviders()
        .AddSignInManager<SignInManager<ApplicationUser>>();

        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"]
            ?? throw new InvalidOperationException("JWT SecretKey not configured.");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        services.AddAuthorization();

        services.AddSharedKernel(Assembly.GetExecutingAssembly());

        // Services
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();

        return services;
    }

    public static WebApplication UseIdentityModule(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapIdentityEndpoints();
        return app;
    }
}