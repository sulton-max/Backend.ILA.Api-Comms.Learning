namespace N2.Microservices.Services.BankingService.Domain.Models.Entities;

public class Account
{
    public Guid Id { get; set; }

    public string AccountType { get; set; } = string.Empty;

    public decimal AccountBalance { get; set; }
}