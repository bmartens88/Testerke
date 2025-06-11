namespace Testerke.Common.Application.EventBus;

/// <summary>
///     Defines the contract for an integration event.
/// </summary>
public interface IIntegrationEvent
{
    /// <summary>
    ///     The unique identifier for the integration event.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    ///     The moment in time when the event occurred, in UTC.
    /// </summary>
    DateTime OccurredOnUtc { get; }
}