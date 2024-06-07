using pruebatecnica.Domain.DTOs;

namespace pruebatecnica.Application.Interfaces;

public interface IPaymentRepository
{
    public Task<IEnumerable<PaymentDto>> GetAsync();
    public Task<PaymentDto> GetById(int id);
    public Task<PaymentDto> CreateAsync(PaymentDto payment);
    public Task<PaymentDto> UpdateAsync(int id, PaymentDto payment);
    public Task DeleteAsync(int id);

}
