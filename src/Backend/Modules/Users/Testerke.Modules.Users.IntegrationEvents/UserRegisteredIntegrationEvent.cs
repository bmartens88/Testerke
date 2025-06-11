using Testerke.Common.Application.EventBus;

namespace Testerke.Modules.Users.IntegrationEvents;

/// <summary>
///     Integration event when a new user is registered in the system.
/// </summary>
/// <param name="OccurredOnUtc">Moment in time the event occurred.</param>
/// <param name="UserId">The unique identifier of the registered user.</param>
/// <param name="Email">The email of the user.</param>
/// <param name="FirstName">The first name of the user.</param>
/// <param name="LastName">The last name of the user.</param>
public sealed record UserRegisteredIntegrationEvent(
    DateTime OccurredOnUtc,
    Guid UserId,
    string Email,
    string FirstName,
    string LastName)
    : IntegrationEvent(Guid.NewGuid(), OccurredOnUtc);