using MediatR;
using Testerke.Common.Domain.Monads;

namespace Testerke.Common.Application.Messaging;

/// <summary>
///     Defines a contract for handling commands in a CQRS architecture.
/// </summary>
/// <typeparam name="TCommand">Type of the command.</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

/// <summary>
///     Defines a contract for handling commands in a CQRS architecture that returns a response.
/// </summary>
/// <typeparam name="TCommand">Type of the command.</typeparam>
/// <typeparam name="TResponse">Type of the response.</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;