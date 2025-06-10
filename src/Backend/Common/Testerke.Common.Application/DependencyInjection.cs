using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Testerke.Common.Application.Behaviors;

namespace Testerke.Common.Application;

/// <summary>
///     Provides extension methods for configuring the application layer dependencies.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Adds application layer services to specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection" /> used to configure application services.</param>
    /// <param name="assemblies">Assemblies which should be scanned for compatible types for application services.</param>
    /// <returns><see cref="IServiceCollection" /> with configured application services.</returns>
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(assemblies);

            options.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            options.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

        return services;
    }
}