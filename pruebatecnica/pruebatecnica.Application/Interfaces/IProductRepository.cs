using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Application.Interfaces;

public interface IOrderRepository
{
    public Task<IEnumerable<Order>> GetAsync();
    public Task<Order> GetById(int id);
    public Task<Order> CreateAsync(Order order);
    public Task UpdateAsync(int id, Order order);
    public Task DeleteAsync(int id);

}
