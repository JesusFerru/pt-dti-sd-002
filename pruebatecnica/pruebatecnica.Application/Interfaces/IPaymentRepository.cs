using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Application.Interfaces;

public interface IPaymentRepository
{
    public Task<IEnumerable<Payment>> GetAsync();
    public Task<Payment> GetById(int id);
    public Task<Payment> CreateAsync(Payment payment);
    public Task UpdateAsync(int id, Payment payment);
    public Task DeleteAsync(int id);

}
