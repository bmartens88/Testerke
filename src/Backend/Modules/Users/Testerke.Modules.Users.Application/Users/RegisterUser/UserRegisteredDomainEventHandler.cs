using MediatR;
using Testerke.Common.Application.EventBus;
using Testerke.Common.Application.Exceptions;
using Testerke.Common.Application.Messaging;
using Testerke.Modules.Users.Application.Users.GetUser;
using Testerke.Modules.Users.Domain.Users.Events;
using Testerke.Modules.Users.IntegrationEvents;

namespace Testerke.Modules.Users.Application.Users.RegisterUser;

/// <summary>
///     Domain event handler for the <see cref="UserRegisteredDomainEvent" /> domain event.
/// </summary>
/// <param name="sender"><see cref="ISender" /> instance for sending additional commands/queries.</param>
/// <param name="bus"><see cref="IEventBus" /> instance for publishing integration events.</param>
internal sealed class UserRegisteredDomainEventHandler(ISender sender, IEventBus bus)
    : DomainEventHandler<UserRegisteredDomainEvent>
{
    private readonly IEventBus _bus = bus;
    private readonly ISender _sender = sender;

    /// <inheritdoc />
    public override async Task Handle(UserRegisteredDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new GetUserQuery(domainEvent.UserId.Id), cancellationToken);

        if (result.IsFailure)
            throw new TesterkeException(nameof(GetUserQuery), result.Error);

        await _bus.PublishAsync(new UserRegisteredIntegrationEvent(
            domainEvent.OccurredOnUtc,
            result.Value.UserId,
            result.Value.Email,
            result.Value.FirstName,
            result.Value.LastName), cancellationToken);
    }
}