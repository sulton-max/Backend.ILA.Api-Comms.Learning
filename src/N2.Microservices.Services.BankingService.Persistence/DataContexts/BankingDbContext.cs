using Microsoft.EntityFrameworkCore;
using N2.Microservices.Services.BankingService.Domain.Models.Entities;

namespace N2.Microservices.Services.BankingService.Persistence.DataContexts;

public class BankingDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
    {
    }
}