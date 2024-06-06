using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Application.Interfaces;

public interface IShoppingCartRepository
{
    public Task<IEnumerable<ShoppingCart>> GetAsync();
    public Task<ShoppingCart> GetById(int id);
    public Task<ShoppingCart> CreateAsync(ShoppingCart shoppingCart);
    public Task UpdateAsync(int id, ShoppingCart shoppingCart);
    public Task DeleteAsync(int id);

}
