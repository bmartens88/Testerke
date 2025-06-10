using MediatR;
using Testerke.Common.Domain.Monads;

namespace Testerke.Common.Application.Messaging;

/// <summary>
///     Defines a contract for handling queries in a CQRS architecture.
/// </summary>
/// <typeparam name="TQuery">Type of the query.</typeparam>
/// <typeparam name="TResponse">Type of the response.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;