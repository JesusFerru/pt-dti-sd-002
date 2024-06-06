using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<Product> CreateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, Product product)
    {
        throw new NotImplementedException();
    }
}
