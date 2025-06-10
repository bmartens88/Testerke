using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Testerke.Common.Presentation.Endpoints;
using Testerke.Common.Presentation.Results;
using Testerke.Modules.Users.Application.Users.GetUser;

namespace Testerke.Modules.Users.Presentation.Users;

internal sealed class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/profile/{userId:guid}",
                async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetUserQuery(userId), cancellationToken);
                    return result.Match(Results.Ok, ApiResults.Problem);
                })
            .WithTags(Tags.Users);
    }
}