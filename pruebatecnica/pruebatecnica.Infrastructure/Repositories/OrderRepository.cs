using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<IEnumerable<Order>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetById(int id)
    {
        throw new NotImplementedException();
    }
    public Task<Order> CreateAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, Order order)
    {
        throw new NotImplementedException();
    }
}
