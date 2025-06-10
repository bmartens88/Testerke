using MediatR;

namespace Testerke.Common.Domain.Abstractions;

/// <summary>
///     Defines the contract for a domain event in the system.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    ///     The moment in time (UTC) when the event occurred.
    /// </summary>
    DateTime OccurredOnUtc { get; }
}