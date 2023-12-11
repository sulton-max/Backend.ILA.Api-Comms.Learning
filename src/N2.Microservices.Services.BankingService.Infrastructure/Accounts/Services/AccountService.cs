using N2.Microservices.Services.BankingService.Application.Accounts.Services;
using N2.Microservices.Services.BankingService.Domain.Models.Entities;
using N2.Microservices.Services.BankingService.Persistence.Repositories.Interfaces;

namespace N2.Microservices.Services.BankingService.Infrastructure.Accounts.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public ValueTask<IList<Account>> GetAccountsAsync()
    {
        return _accountRepository.GetAccounts();
    }

    public ValueTask<bool> TransferAsync(Guid fromUserId, Guid toUserId, decimal amount)
    {
        throw new NotImplementedException();
    }
}