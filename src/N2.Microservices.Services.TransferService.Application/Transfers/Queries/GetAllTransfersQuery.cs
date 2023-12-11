using N2.Microservices.Common.Core.Models.Queries;
using N2.Microservices.Services.TransferService.Domain.Models;
using N2.Microservices.Services.TransferService.Domain.Models.Entities;

namespace N2.Microservices.Services.TransferService.Application.Transfers.Queries;

public class GetAllTransfersQuery : IQuery<IList<Transfer>>
{
}