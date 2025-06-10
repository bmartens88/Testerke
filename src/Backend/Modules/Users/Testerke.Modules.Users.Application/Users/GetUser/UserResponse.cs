namespace Testerke.Modules.Users.Application.Users.GetUser;

public record UserResponse(Guid UserId, string Email, string FirstName, string LastName);