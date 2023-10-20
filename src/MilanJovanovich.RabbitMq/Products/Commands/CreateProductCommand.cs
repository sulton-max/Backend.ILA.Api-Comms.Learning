using MediatR;
using MilanJovanovich.RabbitMq.Models.Entities;

namespace MilanJovanovich.RabbitMq.Products.Commands;

public class CreateProductCommand : IRequest<Product>
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}