using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    public Task<Payment> CreateAsync(Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Payment>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Payment> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, Payment payment)
    {
        throw new NotImplementedException();
    }
}
