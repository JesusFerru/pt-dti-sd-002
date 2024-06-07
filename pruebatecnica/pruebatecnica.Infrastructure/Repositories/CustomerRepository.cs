using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pruebatecnica.Application.Interfaces;
using pruebatecnica.Application.Services;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;
using pruebatecnica.Infrastructure.Data;

namespace pruebatecnica.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{

    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CustomerRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CreateCustomerDto> CreateAsync(CreateCustomerDto customerDto)
    {
        try
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(customerDto.Password);

            var customer = new Customer
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                Password = hashedPassword,
                PhoneNumber = customerDto.PhoneNumber,
                CreatedAt = DateTime.Now
            };

            _context.customers.Add(customer);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<CreateCustomerDto>(customer);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in CreateAsync: {e.Message}");
            throw;
        }
    }

    public async Task<Customer> LoginAsync(LoginCustomerDto loginDto)
    {
        try
        {
            var customer = await _context.customers.FirstOrDefaultAsync(c => c.Email == loginDto.Email);

            if (customer != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, customer.Password))
            {
                return customer; // La contraseña coincide con la registrada
            }
            else
            {
                return null; // Las contraseña no coincide o el correo del cliente no existe
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in LoginAsync: {e.Message}");
            throw;
        }
    }
}
