using System.Linq.Expressions;
using FileBaseContext.Set.Models.FileSet;
using MilanJovanovich.RabbitMq.Models.Entities;
using MilanJovanovich.RabbitMq.Services.Interfaces;

namespace MilanJovanovich.RabbitMq.Services;

public class ProductService : IProductService
{
    private readonly FileSet<Product, Guid> _products = new(nameof(_products), null, null);

    public IQueryable<Product> Get(Expression<Func<Product, bool>> predicate)
    {
        return _products.Where(predicate.Compile()).AsQueryable();
    }

    public ValueTask<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _products.FindAsync(id, cancellationToken);
    }

    public async ValueTask<Product> CreateAsync(Product product, bool save = true, CancellationToken cancellationToken = default)
    {
        product.Id = Guid.NewGuid();
        var createdProduct = await _products.AddAsync(product, cancellationToken);

        if (save)
            await _products.SaveChangesAsync(cancellationToken);

        return createdProduct.Entity;
    }
}