using MassTransit;
using Testerke.Common.Application.EventBus;

namespace Testerke.Common.Infrastructure.EventBus;

/// <summary>
///     Implementation of the event bus.
/// </summary>
/// <param name="bus"><see cref="IBus" /> instance for publishing events.</param>
internal sealed class EventBus(IBus bus) : IEventBus
{
    private readonly IBus _bus = bus;

    /// <inheritdoc />
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent
    {
        await _bus.Publish(integrationEvent, cancellationToken);
    }
}