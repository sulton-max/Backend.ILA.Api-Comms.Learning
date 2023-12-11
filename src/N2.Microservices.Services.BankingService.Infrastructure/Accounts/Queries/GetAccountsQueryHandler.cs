using MediatR;
using N2.Microservices.Common.Core.Models.Queries;
using N2.Microservices.Services.BankingService.Application.Accounts.Queries;
using N2.Microservices.Services.BankingService.Application.Accounts.Services;
using N2.Microservices.Services.BankingService.Domain.Models.Entities;

namespace N2.Microservices.Services.BankingService.Infrastructure.Accounts.Queries;

public class GetAccountsQueryHandler : IQueryHandler<GetAccountsQuery, IList<Account>>
{
    private readonly IAccountService _accountService;

    public GetAccountsQueryHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public Task<IList<Account>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        return _accountService.GetAccountsAsync().AsTask();
    }
}