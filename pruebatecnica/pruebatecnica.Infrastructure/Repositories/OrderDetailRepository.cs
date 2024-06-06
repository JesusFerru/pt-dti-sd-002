using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Infrastructure.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    public Task<OrderDetail> CreateAsync(OrderDetail orderDetail)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDetail>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderDetail> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, OrderDetail orderDetail)
    {
        throw new NotImplementedException();
    }
}
