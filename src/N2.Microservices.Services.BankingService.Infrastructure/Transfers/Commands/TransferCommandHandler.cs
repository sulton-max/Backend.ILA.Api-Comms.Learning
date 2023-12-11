using N2.Microservices.Common.Core.Models.Commands;
using N2.Microservices.EventBus.EventBus;
using N2.Microservices.Services.BankingService.Application.Transfers.Commands;
using N2.Microservices.Services.BankingService.Application.Transfers.Events;

namespace N2.Microservices.Services.BankingService.Infrastructure.Transfers.Commands;

public class TransferCommandHandler : ICommandHandler<TransferCommand, bool>
{
    private readonly IEventBusBroker _eventBusBroker;

    public TransferCommandHandler(IEventBusBroker eventBusBroker)
    {
        _eventBusBroker = eventBusBroker;
    }

    public async Task<bool> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
        var transferAccountEvent = new TransferEvent(request.FromUserId, request.ToUserId, request.Amount);
        await _eventBusBroker.PublishAsync(transferAccountEvent);

        return true;
    }
}