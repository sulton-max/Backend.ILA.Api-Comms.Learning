using MediatR;

namespace N2.Microservices.Common.Core.Models.Queries;

public interface IQuery<out TResult> : IRequest<TResult>
{
}