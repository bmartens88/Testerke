using Testerke.Domain.Users.ValueObjects;
using Testerke.SharedKernel.Events;

namespace Testerke.Domain.Users.Events;

public sealed record UserRegisteredDomainEvent(UserId UserId): DomainEvent;