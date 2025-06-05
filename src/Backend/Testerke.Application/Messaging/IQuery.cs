using MediatR;
using Testerke.SharedKernel.Monads;

namespace Testerke.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;