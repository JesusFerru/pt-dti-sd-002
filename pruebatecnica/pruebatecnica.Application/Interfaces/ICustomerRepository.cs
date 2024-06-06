using pruebatecnica.Domain.DTOs;

namespace pruebatecnica.Application.Interfaces
{
    public interface ICustomerRepository
    {
        public Task CreateAsync(CreateCustomerDto customerDto);
        public Task LoginAsync(LoginCustomerDto loginDto);
    }
}
