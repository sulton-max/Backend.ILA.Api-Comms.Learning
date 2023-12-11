using N2.Microservices.Common.Core.Models.Queries;
using N2.Microservices.Services.TransferService.Application.Transfers.Queries;
using N2.Microservices.Services.TransferService.Application.Transfers.Services;
using N2.Microservices.Services.TransferService.Domain.Models;
using N2.Microservices.Services.TransferService.Domain.Models.Entities;

namespace N2.Microservices.Services.TransferService.Infrastructure.Transfers.Queries;

public class GetAllTransfersQueryHandler : IQueryHandler<GetAllTransfersQuery, IList<Transfer>>
{
    private readonly ITransferService _transferService;

    public GetAllTransfersQueryHandler(ITransferService transferService)
    {
        _transferService = transferService;
    }

    public async Task<IList<Transfer>> Handle(GetAllTransfersQuery request, CancellationToken cancellationToken)
    {
        return await _transferService.GetAllTransfersAsync();
    }
}