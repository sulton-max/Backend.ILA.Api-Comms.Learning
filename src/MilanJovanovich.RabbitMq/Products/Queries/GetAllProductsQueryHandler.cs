using MediatR;
using MilanJovanovich.RabbitMq.Models.Entities;
using MilanJovanovich.RabbitMq.Services;
using MilanJovanovich.RabbitMq.Services.Interfaces;

namespace MilanJovanovich.RabbitMq.Products.Queries;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IList<Product>>
{
    private readonly IProductService _productService;

    public GetAllProductsQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public Task<IList<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = _productService.Get(product => true).ToList();
        return Task.FromResult<IList<Product>>(products);
    }
}