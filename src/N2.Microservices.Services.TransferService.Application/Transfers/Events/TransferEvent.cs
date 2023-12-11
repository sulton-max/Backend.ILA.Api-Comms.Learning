using N2.Microservices.Common.Core.Models.Events;

namespace N2.Microservices.Services.TransferService.Application.Transfers.Events;

public class TransferEvent : IEvent
{
    public Guid FromUserId { get; set; }

    public Guid ToUserId { get; set; }

    public decimal Amount { get; set; }

    public TransferEvent(Guid requestFromUserId, Guid requestToUserId, decimal requestAmount)
    {
        FromUserId = requestFromUserId;
        ToUserId = requestToUserId;
        Amount = requestAmount;
    }
}