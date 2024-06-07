using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.DTOs.Sales;
using pruebatecnica.Domain.Entities;
using pruebatecnica.Domain.Wrapper;

namespace pruebatecnica.Application.Interfaces.Services;

public interface IOrderService
{
    Task<ApiResponse<OrderResultDto>> GenerateOrder(int customerId);
    Task<ApiResponse<IEnumerable<OrderDto>>> GetCustomerOrders(int customerId);
    Task<ApiResponse<IEnumerable<OrderDto>>> GetOrderByStatus(string statusOrder);
    Task<ApiResponse<bool>> CreatePayment(PaymentDto paymentDto);
}
