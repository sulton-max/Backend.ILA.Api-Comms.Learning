using Microsoft.EntityFrameworkCore;
using N2.Microservices.Services.BankingService.Domain.Models.Entities;
using N2.Microservices.Services.BankingService.Persistence.DataContexts;
using N2.Microservices.Services.BankingService.Persistence.Repositories.Interfaces;

namespace N2.Microservices.Services.BankingService.Persistence.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BankingDbContext _bankingDbContext;

    public AccountRepository(BankingDbContext bankingDbContext)
    {
        _bankingDbContext = bankingDbContext;
    }

    public async ValueTask<IList<Account>> GetAccounts()
    {
        return await _bankingDbContext.Accounts.ToListAsync();
    }
}