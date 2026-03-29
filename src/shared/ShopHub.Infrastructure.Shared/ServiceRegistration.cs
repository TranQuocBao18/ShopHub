using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShopHub.Infrastructure.Shared.Behaviors;
using ShopHub.Infrastructure.Shared.Interfaces;
using ShopHub.Infrastructure.Shared.Services;
using System.Reflection;

namespace ShopHub.Infrastructure.Shared;

public static class ServiceRegistration
{
    public static IServiceCollection AddSharedKernel(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(assemblies);
        services.AddTransient<IDateTimeService, DateTimeService>();

        return services;
    }
}