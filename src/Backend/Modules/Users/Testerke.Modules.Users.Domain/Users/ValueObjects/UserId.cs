using Testerke.Common.Domain;

namespace Testerke.Modules.Users.Domain.Users.ValueObjects;

/// <summary>
///     Strongly typed identifier for a user.
/// </summary>
public sealed class UserId : TypedId<Guid>
{
    /// <summary>
    ///     Construct a new instance of <see cref="UserId" /> with the specified value.
    /// </summary>
    /// <param name="value"><see cref="Guid" /> value.</param>
    private UserId(Guid value)
        : base(value)
    {
    }

    /// <summary>
    ///     Creates a new instance of <see cref="UserId" /> with the specified value.
    /// </summary>
    /// <param name="value"><see cref="Guid" /> value.</param>
    /// <returns>New instance of <see cref="UserId" />.</returns>
    public static UserId Create(Guid value)
    {
        return new UserId(value);
    }

    /// <summary>
    ///     Creates a new instance of <see cref="UserId" /> with a new <see cref="Guid" /> value.
    /// </summary>
    /// <returns>New instance of <see cref="UserId" />.</returns>
    public static UserId Create()
    {
        return new UserId(Guid.NewGuid());
    }
}