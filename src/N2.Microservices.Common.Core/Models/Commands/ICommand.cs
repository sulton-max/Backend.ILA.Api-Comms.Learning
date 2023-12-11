using MediatR;

namespace N2.Microservices.Common.Core.Models.Commands;

public interface ICommand<out TResult> : ICommand, IRequest<TResult>
{
}

public interface ICommand
{
}