using Testerke.Common.Application.Messaging;

namespace Testerke.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;