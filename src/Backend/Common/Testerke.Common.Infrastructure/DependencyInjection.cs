using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testerke.Common.Application.EventBus;
using Testerke.Common.Infrastructure.Data;

namespace Testerke.Common.Infrastructure;

/// <summary>
///     Provides extension methods for configuring the infrastructure layer dependencies.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Adds infrastructure layer services to specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection" /> used to configure infrastructure services.</param>
    /// <returns><see cref="IServiceCollection" /> with configured infrastructure services.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.TryAddSingleton<IEventBus, EventBus.EventBus>();
        
        services.TryAddSingleton<DomainEventsInterceptor>();

        return services;
    }
}