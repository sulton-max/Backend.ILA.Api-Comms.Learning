using MediatR;

namespace N2.Microservices.Common.Core.Models.Commands;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
}