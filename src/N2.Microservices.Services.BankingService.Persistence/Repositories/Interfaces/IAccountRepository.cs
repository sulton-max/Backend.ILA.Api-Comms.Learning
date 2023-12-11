using N2.Microservices.Services.BankingService.Domain.Models.Entities;

namespace N2.Microservices.Services.BankingService.Persistence.Repositories.Interfaces;

public interface IAccountRepository
{
     ValueTask<IList<Account>> GetAccounts();
}