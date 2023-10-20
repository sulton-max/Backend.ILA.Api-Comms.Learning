using MediatR;
using MilanJovanovich.RabbitMq.Models.Entities;

namespace MilanJovanovich.RabbitMq.Products.Queries;

public class GetAllProductsQuery : IRequest<IList<Product>>
{
}