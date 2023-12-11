using MediatR;

namespace N2.Microservices.Common.Core.Models.Queries;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
}