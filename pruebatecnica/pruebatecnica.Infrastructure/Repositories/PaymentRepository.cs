using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    public Task<PaymentDto> CreateAsync(PaymentDto payment)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PaymentDto>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PaymentDto> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentDto> UpdateAsync(int id, PaymentDto payment)
    {
        throw new NotImplementedException();
    }
}
