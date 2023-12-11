using N2.Microservices.Services.TransferService.Application.Transfers.Services;
using N2.Microservices.Services.TransferService.Domain.Models;
using N2.Microservices.Services.TransferService.Domain.Models.Entities;
using N2.Microservices.Services.TransferService.Persistence.Repositories.Interfaces;

namespace N2.Microservices.Services.TransferService.Infrastructure.Transfers.Services;

public class TransferService : ITransferService
{
    private readonly ITransferRepository _transferRepository;

    public TransferService(ITransferRepository transferRepository)
    {
        _transferRepository = transferRepository;
    }

    public ValueTask<IList<Transfer>> GetAllTransfersAsync()
    {
        return _transferRepository.GetAllTransfersAsync();
    }
}