using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Application.Interfaces;

public interface ICustomerRepository
{
    public Task<CreateCustomerDto> CreateAsync(CreateCustomerDto customerDto);
    public Task<Customer> LoginAsync(LoginCustomerDto loginDto);
}
