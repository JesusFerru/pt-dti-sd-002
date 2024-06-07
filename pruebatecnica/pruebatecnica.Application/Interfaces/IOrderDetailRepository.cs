using pruebatecnica.Domain.DTOs;

namespace pruebatecnica.Application.Interfaces;

public interface IOrderDetailRepository
{
    public Task<IEnumerable<OrderDetailDto>> GetAsync();
    public Task<OrderDetailDto> GetById(int id);
    public Task<IEnumerable<OrderDetailDto>> GetByOrderId(int OrderId);
    public Task<OrderDetailDto> CreateAsync(OrderDetailDto orderDetail);
    public Task<OrderDetailDto> UpdateAsync(int id, OrderDetailDto orderDetailDto);
    public Task DeleteAsync(int id);

}
