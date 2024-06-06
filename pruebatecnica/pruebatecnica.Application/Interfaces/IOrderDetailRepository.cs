using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Application.Interfaces;

public interface IOrderDetailRepository
{
    public Task<IEnumerable<OrderDetail>> GetAsync();
    public Task<OrderDetail> GetById(int id);
    public Task<OrderDetail> CreateAsync(OrderDetail orderDetail);
    public Task UpdateAsync(int id, OrderDetail orderDetail);
    public Task DeleteAsync(int id);

}
