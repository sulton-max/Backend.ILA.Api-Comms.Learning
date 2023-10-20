using MediatR;
using MilanJovanovich.RabbitMq.Models.Entities;

namespace MilanJovanovich.RabbitMq.Products.Queries;

public class GetProductByIdQuery : IRequest<Product?>
{
    public Guid Id { get; set; }
}