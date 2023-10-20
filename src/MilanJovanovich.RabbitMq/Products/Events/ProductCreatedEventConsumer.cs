using MassTransit;

namespace MilanJovanovich.RabbitMq.Products.Events;

public class ProductCreatedEventConsumer : IConsumer<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventConsumer> _logger;

    public ProductCreatedEventConsumer(ILogger<ProductCreatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        _logger.LogInformation("Product created {@Product}", context.Message);

        return Task.CompletedTask;
    }
}