using Testerke.SharedKernel.Abstractions;

namespace Testerke.SharedKernel.Events;

public abstract record DomainEvent(DateTime OccurredOnUtc) : IDomainEvent
{
    protected DomainEvent() : this(DateTime.UtcNow)
    {
    }
}