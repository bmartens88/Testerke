using MediatR;
using Testerke.SharedKernel.Monads;

namespace Testerke.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;