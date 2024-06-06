using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Infrastructure.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    public Task<ShoppingCart> CreateAsync(ShoppingCart shoppingCart)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ShoppingCart>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ShoppingCart> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, ShoppingCart shoppingCart)
    {
        throw new NotImplementedException();
    }
}
