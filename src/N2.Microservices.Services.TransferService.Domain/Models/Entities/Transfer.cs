namespace N2.Microservices.Services.TransferService.Domain.Models.Entities;

public class Transfer
{
    public Guid Id { get; set; }

    public Guid FromAccountId { get; set; }

    public Guid ToAccountId { get; set; }

    public decimal Amount { get; set; }

    public DateTimeOffset CreatedTime { get; set; }
}