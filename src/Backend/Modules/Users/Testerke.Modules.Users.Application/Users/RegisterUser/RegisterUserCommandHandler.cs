using Testerke.Common.Application.Messaging;
using Testerke.Common.Domain.Monads;
using Testerke.Modules.Users.Application.Abstractions;
using Testerke.Modules.Users.Domain.Users;
using Testerke.Modules.Users.Domain.Users.Abstractions;

namespace Testerke.Modules.Users.Application.Users.RegisterUser;

/// <summary>
///     Handles the <see cref="RegisterUserCommand" /> command.
/// </summary>
/// <param name="userRepository"><see cref="IUserRepository" /> instance for persistence access.</param>
/// <param name="unitOfWork"><see cref="IUnitOfWork" /> for saving changes to underlying persistence store.</param>
internal sealed class RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;

    /// <inheritdoc />
    public async Task<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = User.Create(command.Email, command.FirstName, command.LastName);

        _userRepository.Insert(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}