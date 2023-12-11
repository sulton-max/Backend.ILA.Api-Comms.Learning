using N2.Microservices.Services.TransferService.Domain.Models;
using N2.Microservices.Services.TransferService.Domain.Models.Entities;

namespace N2.Microservices.Services.TransferService.Application.Transfers.Services;

public interface ITransferService
{
    ValueTask<IList<Transfer>> GetAllTransfersAsync();
}