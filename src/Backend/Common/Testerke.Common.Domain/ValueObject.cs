namespace Testerke.Common.Domain;

/// <summary>
///     Provides a base class for value objects that implements equality based on their properties.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <inheritdoc />
    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }

    /// <summary>
    ///     Get the components that determine the equality of this value object.
    /// </summary>
    /// <returns>
    ///     A sequence of components that defines the equality of the object.
    /// </returns>
    /// <remarks>
    ///     Implementers should override this method and <c>yield return</c> the properties
    ///     that are significant for equality comparison. This method is used by the
    ///     base class's implementation of <see cref="Equals(object)" /> and <see cref="GetHashCode()" />.
    /// </remarks>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ValueObject other &&
               GetEqualityComponents()
                   .SequenceEqual(other.GetEqualityComponents());
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    ///     Equality operator for value objects.
    /// </summary>
    /// <param name="left"><see cref="ValueObject" /> instance to compare.</param>
    /// <param name="right"><see cref="ValueObject" /> instance to compare.</param>
    /// <returns><c>true</c> if both <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    ///     Non-equality operator for value objects.
    /// </summary>
    /// <param name="left"><see cref="ValueObject" /> instance to compare.</param>
    /// <param name="right"><see cref="ValueObject" /> instance to compare.</param>
    /// <returns>
    ///     <c>true</c> if both <paramref name="left" /> and <paramref name="right" /> are non-equal; otherwise,
    ///     <c>false</c>.
    /// </returns>
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !Equals(left, right);
    }
}