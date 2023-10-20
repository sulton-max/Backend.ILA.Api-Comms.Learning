using FileBaseContext.Abstractions.Models.Entity;

namespace MilanJovanovich.RabbitMq.Models.Entities;

public class Product : IFileSetEntity<Guid>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}