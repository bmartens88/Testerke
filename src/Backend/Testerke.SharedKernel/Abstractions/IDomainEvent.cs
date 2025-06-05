using MediatR;

namespace Testerke.SharedKernel.Abstractions;

public interface IDomainEvent : INotification
{
    DateTime OccurredOnUtc { get; }
}