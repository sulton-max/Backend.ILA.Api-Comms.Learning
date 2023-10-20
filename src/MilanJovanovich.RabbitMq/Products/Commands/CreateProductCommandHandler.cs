using MassTransit;
using MediatR;
using MilanJovanovich.RabbitMq.Messaging;
using MilanJovanovich.RabbitMq.Models.Entities;
using MilanJovanovich.RabbitMq.Products.Events;
using MilanJovanovich.RabbitMq.Services;
using MilanJovanovich.RabbitMq.Services.Interfaces;

namespace MilanJovanovich.RabbitMq.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductService _productService;
    private readonly IEventBus _eventBus;

    public CreateProductCommandHandler(IProductService productService, IEventBus eventBus)
    {
        _productService = productService;
        _eventBus = eventBus;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description
        };

        var createdProduct = await _productService.CreateAsync(product, cancellationToken: cancellationToken);

        await _eventBus.PublishAsync(new ProductCreatedEvent
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                Description = createdProduct.Description
            },
            cancellationToken);

        return createdProduct;
    }
}