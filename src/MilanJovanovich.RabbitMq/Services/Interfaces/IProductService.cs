using System.Linq.Expressions;
using MilanJovanovich.RabbitMq.Models.Entities;

namespace MilanJovanovich.RabbitMq.Services.Interfaces;

public interface IProductService
{
    IQueryable<Product> Get(Expression<Func<Product, bool>> predicate);

    ValueTask<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Product> CreateAsync(Product product, bool save = true, CancellationToken cancellationToken = default);
}