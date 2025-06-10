using Ardalis.GuardClauses;

namespace Testerke.Common.Domain;

/// <summary>
///     Provides a base class for typed identifiers.
/// </summary>
/// <param name="id">The value to store in the typed identifier.</param>
/// <typeparam name="TId">The type of the stored value.</typeparam>
public abstract class TypedId<TId>(TId id) : TypedId
    where TId : struct
{
    public TId Id { get; } = Guard.Against.Default(id);

    /// <inheritdoc />
    protected sealed override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Id;
    }
}

/// <summary>
///     Serves as an abstraction for typed identifiers.
/// </summary>
public abstract class TypedId : ValueObject;