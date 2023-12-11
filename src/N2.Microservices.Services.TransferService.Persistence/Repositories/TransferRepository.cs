using Microsoft.EntityFrameworkCore;
using N2.Microservices.Services.TransferService.Domain.Models;
using N2.Microservices.Services.TransferService.Domain.Models.Entities;
using N2.Microservices.Services.TransferService.Persistence.DataContexts;
using N2.Microservices.Services.TransferService.Persistence.Repositories.Interfaces;

namespace N2.Microservices.Services.TransferService.Persistence.Repositories;

public class TransferRepository : ITransferRepository
{
    private readonly TransferDbContext _transferDbContext;

    public TransferRepository(TransferDbContext transferDbContext)
    {
        _transferDbContext = transferDbContext;
    }

    public async ValueTask<IList<Transfer>> GetAllTransfersAsync()
    {
        return await _transferDbContext.Transfers.ToListAsync();
    }
}