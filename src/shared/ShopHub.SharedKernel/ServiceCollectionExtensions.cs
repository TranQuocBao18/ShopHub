using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShopHub.SharedKernel.Infrastructure.Behaviors;
using System.Reflection;

namespace ShopHub.SharedKernel;

public static class ServiceCollectionExtensions
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

        return services;
    }
}