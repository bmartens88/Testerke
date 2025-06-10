using MediatR;
using Testerke.Common.Domain.Monads;

namespace Testerke.Common.Application.Messaging;

/// <summary>
///     Defines the contract for a command in the application layer.
/// </summary>
public interface ICommand : IRequest<Result>, IBaseCommand;

/// <summary>
///     Defines the contract for a command in the application layer that returns a response.
/// </summary>
/// <typeparam name="TResponse">Type of the response.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

/// <summary>
///     Serves as an abstraction for a base command interface.
/// </summary>
public interface IBaseCommand;