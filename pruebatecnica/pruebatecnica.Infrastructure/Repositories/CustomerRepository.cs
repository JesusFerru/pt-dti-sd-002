using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;

namespace pruebatecnica.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public Task CreateAsync(CreateCustomerDto customerDto)
    {
        throw new NotImplementedException();
    }

    public Task LoginAsync(LoginCustomerDto loginDto)
    {
        throw new NotImplementedException();
    }
}
