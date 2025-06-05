using MediatR;
using Testerke.Api.Endpoints;

namespace Testerke.Api.Users;

internal sealed class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/profile", async (ISender sender) =>
            {
                // TODO: implement
            })
            .WithTags(Tags.Users);
    }
}