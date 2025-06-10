using Microsoft.AspNetCore.Routing;

namespace Testerke.Common.Presentation.Endpoints;

/// <summary>
///     Defines the contract for an endpoint that can be mapped to an <see cref="IEndpointRouteBuilder" />.
/// </summary>
public interface IEndpoint
{
    /// <summary>
    ///     Maps the endpoint to the specified <see cref="IEndpointRouteBuilder" />.
    /// </summary>
    /// <param name="app"><see cref="IEndpointRouteBuilder" /> which will contain the endpoint.</param>
    void MapEndpoint(IEndpointRouteBuilder app);
}