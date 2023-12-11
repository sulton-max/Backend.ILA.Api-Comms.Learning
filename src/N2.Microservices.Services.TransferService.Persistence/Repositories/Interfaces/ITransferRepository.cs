using N2.Microservices.Services.TransferService.Domain.Models;
using N2.Microservices.Services.TransferService.Domain.Models.Entities;

namespace N2.Microservices.Services.TransferService.Persistence.Repositories.Interfaces;

public interface ITransferRepository
{
    ValueTask<IList<Transfer>> GetAllTransfersAsync();
}