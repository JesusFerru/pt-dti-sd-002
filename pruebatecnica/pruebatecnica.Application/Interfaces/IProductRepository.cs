using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Application.Interfaces;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetAsync();
    public Task<Product> GetById(int id);
    public Task<Product> CreateAsync(ProductDto product);
    public Task<Product> UpdateAsync(int id, ProductDto product);
    public Task DeleteAsync(int id);

}
