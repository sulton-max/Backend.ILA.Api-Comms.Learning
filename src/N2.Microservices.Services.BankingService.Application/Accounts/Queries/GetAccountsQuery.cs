using MediatR;
using N2.Microservices.Common.Core.Models.Queries;
using N2.Microservices.Services.BankingService.Domain.Models.Entities;

namespace N2.Microservices.Services.BankingService.Application.Accounts.Queries;

public record GetAccountsQuery : IQuery<IList<Account>>
{
}