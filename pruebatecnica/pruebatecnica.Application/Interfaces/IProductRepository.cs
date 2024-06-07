using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Application.Interfaces;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetAsync();
    public Task<Product> GetById(int id);
    public Task<Product> CreateAsync(Product product);
    public Task<Product> UpdateAsync(int id, Product product);
    public Task DeleteAsync(int id);

}
