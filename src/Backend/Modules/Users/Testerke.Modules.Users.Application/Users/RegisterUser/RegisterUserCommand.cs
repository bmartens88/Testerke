using Testerke.Common.Application.Messaging;

namespace Testerke.Modules.Users.Application.Users.RegisterUser;

/// <summary>
///     Command to register a new user in the system.
/// </summary>
/// <param name="Email">The email of the user.</param>
/// <param name="FirstName">The first name of the user.</param>
/// <param name="LastName">The last name of the user.</param>
public sealed record RegisterUserCommand(string Email, string FirstName, string LastName) : ICommand;