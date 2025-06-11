using Testerke.Common.Domain.Events;
using Testerke.Modules.Users.Domain.Users.ValueObjects;

namespace Testerke.Modules.Users.Domain.Users.Events;

/// <summary>
///     Domain event when a new user is registered in the system.
/// </summary>
/// <param name="UserId">The strongly typed identifier of the user.</param>
public sealed record UserRegisteredDomainEvent(
    UserId UserId) : DomainEvent;