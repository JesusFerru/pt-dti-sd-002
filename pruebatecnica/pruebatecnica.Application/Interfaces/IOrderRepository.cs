using pruebatecnica.Domain.DTOs;
namespace pruebatecnica.Application.Interfaces;

public interface IOrderRepository
{
    public Task<IEnumerable<OrderDto>> GetAsync();
    public Task<OrderDto> GetById(int id);
    public Task<OrderDto> CreateAsync(OrderDto order);
    public Task<OrderDto> UpdateAsync(int id, OrderDto order);
    public Task DeleteAsync(int id);

}
