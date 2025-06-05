using Testerke.Domain.Users.Events;
using Testerke.Domain.Users.ValueObjects;
using Testerke.SharedKernel;

namespace Testerke.Domain.Users;

public sealed class User : AggregateRoot<UserId>
{
    private User(
        UserId id,
        string email,
        string firstName,
        string lastName,
        string identityId)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        IdentityId = identityId;
    }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string IdentityId { get; private set; }

    public static User Create(
        string email,
        string firstName,
        string lastName,
        string identityId,
        UserId? id = null)
    {
        var user = new User(id ?? UserId.CreateNew(), email, firstName, lastName, identityId);
        user.RaiseEvent(new UserRegisteredDomainEvent(user.Id));

        return user;
    }

    public void Update(
        string firstName,
        string lastName)
    {
        if (FirstName == firstName && LastName == lastName)
            return;

        FirstName = firstName;
        LastName = lastName;

        RaiseEvent(new UserProfileUpdatedDomainEvent(Id, FirstName, LastName));
    }
}