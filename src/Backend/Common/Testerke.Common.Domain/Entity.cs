namespace Testerke.Common.Domain;

/// <summary>
///     Provides a base class for an entity that implements equality based on its identifier.
/// </summary>
/// <typeparam name="TId">The type of the strongly typed identifier.</typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : TypedId
{
    /// <summary>
    ///     Constructs a new instance of the <see cref="Entity{TId}" /> class with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier for this entity.</param>
    protected Entity(TId id)
    {
        Id = id;
    }

#pragma warning disable CS8618
    /// <summary>
    ///     Constructs a new instance of the <see cref="Entity{TId}" /> class.
    /// </summary>
    protected Entity()
    {
    }
#pragma warning restore CS8618

    public TId Id { get; set; }

    /// <inheritdoc />
    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> other &&
               Id.Equals(other.Id);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    ///     Equality operator for entities.
    /// </summary>
    /// <param name="left"><see cref="ValueObject" /> instance to compare.</param>
    /// <param name="right"><see cref="ValueObject" /> instance to compare.</param>
    /// <returns><c>true</c> if both <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    ///     Non-equality operator for entities.
    /// </summary>
    /// <param name="left"><see cref="ValueObject" /> instance to compare.</param>
    /// <param name="right"><see cref="ValueObject" /> instance to compare.</param>
    /// <returns>
    ///     <c>true</c> if both <paramref name="left" /> and <paramref name="right" /> are non-equal; otherwise,
    ///     <c>false</c>.
    /// </returns>
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !Equals(left, right);
    }
}