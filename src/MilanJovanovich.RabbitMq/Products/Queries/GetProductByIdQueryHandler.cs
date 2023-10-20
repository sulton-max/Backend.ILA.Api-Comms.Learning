using MediatR;
using MilanJovanovich.RabbitMq.Models.Entities;
using MilanJovanovich.RabbitMq.Services.Interfaces;

namespace MilanJovanovich.RabbitMq.Products.Queries;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IProductService _productService;

    public GetProductByIdQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return _productService.GetByIdAsync(request.Id, cancellationToken).AsTask();
    }
}