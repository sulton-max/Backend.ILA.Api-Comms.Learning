using N2.Microservices.Common.Core.Models.Commands;

namespace N2.Microservices.Services.BankingService.Application.Transfers.Commands;

public class TransferCommand : ICommand<bool>
{
    public Guid FromUserId { get; set; }

    public Guid ToUserId { get; set; }

    public decimal Amount { get; set; }
}