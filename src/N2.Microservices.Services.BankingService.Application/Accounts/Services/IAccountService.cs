using N2.Microservices.Services.BankingService.Domain.Models.Entities;

namespace N2.Microservices.Services.BankingService.Application.Accounts.Services;

public interface IAccountService
{
    ValueTask<IList<Account>> GetAccountsAsync();

    ValueTask<bool> TransferAsync(Guid fromUserId, Guid toUserId, decimal amount);
}