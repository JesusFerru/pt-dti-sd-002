using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<OrderDto> CreateAsync(OrderDto order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDto>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto> UpdateAsync(int id, OrderDto order)
    {
        throw new NotImplementedException();
    }
}
