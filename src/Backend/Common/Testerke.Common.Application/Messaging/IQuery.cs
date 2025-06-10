using MediatR;
using Testerke.Common.Domain.Monads;

namespace Testerke.Common.Application.Messaging;

/// <summary>
///     Defines the contract for a query in the application layer.
/// </summary>
/// <typeparam name="TResponse">Type of the response.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;