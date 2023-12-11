using Microsoft.EntityFrameworkCore;
using N2.Microservices.Services.TransferService.Domain.Models;
using N2.Microservices.Services.TransferService.Domain.Models.Entities;

namespace N2.Microservices.Services.TransferService.Persistence.DataContexts;

public class TransferDbContext : DbContext
{
    public DbSet<Transfer> Transfers { get; set; }

    public TransferDbContext(DbContextOptions<TransferDbContext> options) : base(options)
    {
    }
}