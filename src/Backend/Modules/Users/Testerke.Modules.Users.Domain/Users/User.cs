using Ardalis.GuardClauses;
using Testerke.Common.Domain;
using Testerke.Common.Domain.Guards;
using Testerke.Modules.Users.Domain.Users.Events;
using Testerke.Modules.Users.Domain.Users.ValueObjects;

namespace Testerke.Modules.Users.Domain.Users;

/// <summary>
///     User aggregate root representing a user in the system.
/// </summary>
public sealed class User : AggregateRoot<UserId>
{
    /// <summary>
    ///     Constructs a new instance of the <see cref="User" /> class with the specified parameters.
    /// </summary>
    /// <param name="id">Strongly typed identifier for this user.</param>
    /// <param name="email">The email of the user.</param>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    private User(
        UserId id,
        string email,
        string firstName,
        string lastName)
        : base(id)
    {
        Email = Guard.Against.Length(email, 100);
        FirstName = Guard.Against.Length(firstName, 100);
        LastName = Guard.Against.Length(lastName, 200);
    }

    public string Email { get; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    /// <summary>
    ///     Factory method to create a new user instance.
    /// </summary>
    /// <param name="email">Email of the user.</param>
    /// <param name="firstName">First name of the user.</param>
    /// <param name="lastName">Last name of the user.</param>
    /// <param name="id">Strongly typed identifier for the user (optional).</param>
    /// <returns>A new instance of <see cref="User" />.</returns>
    public static User Create(
        string email,
        string firstName,
        string lastName,
        UserId? id = null)
    {
        var user = new User(id ?? UserId.Create(), email, firstName, lastName);
        // Raise domain event
        user.Raise(new UserRegisteredDomainEvent(user.Id));
        return user;
    }

    /// <summary>
    ///     Updates the user's first and last name.
    /// </summary>
    /// <param name="firstName">Updated first name.</param>
    /// <param name="lastName">Updated last name.</param>
    public void Update(
        string firstName,
        string lastName)
    {
        if (FirstName == firstName && LastName == lastName)
            return;

        FirstName = Guard.Against.Length(firstName, 100);
        LastName = Guard.Against.Length(lastName, 200);

        // Possibly do something with a domain (integration?) event here, like UserUpdatedEvent
    }
}