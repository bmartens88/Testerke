using Testerke.Common.Application.Messaging;
using Testerke.Common.Domain.Monads;
using Testerke.Modules.Users.Domain.Users.Abstractions;
using Testerke.Modules.Users.Domain.Users.Errors;
using Testerke.Modules.Users.Domain.Users.ValueObjects;

namespace Testerke.Modules.Users.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(IUserRepository userRepository) : IQueryHandler<GetUserQuery, UserResponse>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<UserResponse>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(query.UserId);
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        return user is null
            ? Result.Failure<UserResponse>(UserErrors.NotFound(userId))
            : new UserResponse(user.Id.Id, user.Email, user.FirstName, user.LastName);
    }
}