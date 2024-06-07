using pruebatecnica.Domain.DTOs;

namespace pruebatecnica.Application.Interfaces;

public interface IShoppingCartRepository
{
    public Task<IEnumerable<ShoppingCartDto>> GetAsync();
    public Task<ShoppingCartDto> GetById(int id);
    public Task<IEnumerable<ShoppingCartDto>> GetByCustomerId(int customerId);
    public Task<ShoppingCartDto> CreateAsync(ShoppingCartDto shoppingCart);
    public Task<ShoppingCartDto> UpdateAsync(int id, ShoppingCartDto shoppingCart);
    public Task DeleteAsync(int id);

}
