using FluentValidation;

namespace Testerke.Modules.Users.Application.Users.RegisterUser;

/// <summary>
///     Validator for the <see cref="RegisterUserCommand" /> command.
/// </summary>
internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    /// <summary>
    ///     Constructs a new instance of <see cref="RegisterUserCommandValidator" />.
    /// </summary>
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Email).EmailAddress();
    }
}