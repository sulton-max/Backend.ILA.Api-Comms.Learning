using N2.Microservices.Common.Core.Models.Events;
using N2.Microservices.Services.TransferService.Application.Transfers.Events;

namespace N2.Microservices.Services.TransferService.Infrastructure.Transfers.Events;

public class TransferEventHandler : IEventHandler<TransferEvent>
{
    public Task Handle(TransferEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}