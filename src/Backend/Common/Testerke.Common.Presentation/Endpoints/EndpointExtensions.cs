using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Testerke.Common.Presentation.Endpoints;

/// <summary>
///     Provides extension methods for registering endpoints in the service collection.
/// </summary>
public static class EndpointExtensions
{
    /// <summary>
    ///     Adds all implementations of <see cref="IEndpoint" /> found in the specified assemblies to the service collection.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection" /> for adding <see cref="IEndpoint" /> implementations to.</param>
    /// <param name="assemblies">Array of assemblies to scan for eligible types.</param>
    /// <returns><see cref="IServiceCollection" /> with the eligible types added.</returns>
    public static IServiceCollection AddEndpoints(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        var serviceDescriptors = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    /// <summary>
    ///     Maps all registered endpoints to the application builder.
    /// </summary>
    /// <param name="app"><see cref="WebApplication" /> instance.</param>
    /// <param name="routeGroupBuilder"><see cref="RouteGroupBuilder" /> instance for mapping endpoints.</param>
    /// <returns><see cref="IApplicationBuilder" /> with all endpoints mapped.</returns>
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var endpoint in endpoints)
            endpoint.MapEndpoint(builder);

        return app;
    }
}