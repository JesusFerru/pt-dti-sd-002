using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;
using pruebatecnica.Infrastructure.Data;

namespace pruebatecnica.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> GetAsync()
    {
        try
        {
            var data = await _context.orders.ToListAsync();
            var response = data.Select(x => _mapper.Map<OrderDto>(x)).ToList();
            return response;
        }
        catch (Exception e)
        {

            throw new Exception($"Error in GetAsync: {e.Message}");

        }
    }

    public async Task<OrderDto> GetById(int id)
    {
        try
        {
            var data = await _context.orders
            .FirstOrDefaultAsync(x => x.OrderId == id)
             ?? throw new Exception($"Order with Id '{id}' not found");

            var response = _mapper.Map<OrderDto>(data);
            return response;
        }
        catch (Exception e)
        {
            throw new Exception($"Error in GetById: {e.Message}");
        }
    }

    public async Task<OrderDto> CreateAsync(OrderDto order)
    {
        try
        {
            var customer = await _context.customers.FindAsync(order.CustomerId)
                         ?? throw new Exception("Order not found");

            var data = _mapper.Map<Order>(order);

            data.Customer = customer;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.orders.Add(data);
                    await _context.SaveChangesAsync();

                    // Confirmar la transacción
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // Deshacer la transacción en caso de error o proceso incompleto
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            // Mapear la entidad creada de vuelta al DTO
            var response = _mapper.Map<OrderDto>(data);
            return response;
        }
        catch (Exception e)
        {
            throw new Exception($"Error in CreateAsync: {e.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var data = await _context.orders.FindAsync(id)
                ?? throw new Exception($"Order with Id '{id}' not found");

            _context.orders.Remove(data);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error in DeleteAsync: {e.Message}");
        }
    }

   

    public async Task<OrderDto> UpdateAsync(int id, OrderDto newData)
    {
        try
        {
            var data = await _context.orders.FindAsync(id)
                 ?? throw new Exception($"Order with Id '{id}' not found");

            _mapper.Map(newData, data);

            data.Customer = await _context.customers.FindAsync(newData.OrderId)
                 ?? throw new Exception("Customer not found");

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //Actualizar los datos en el objeto "data" obtenido
                    _context.orders.Update(data);
                    await _context.SaveChangesAsync();
                    //Confirmar la transacción
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // Deshacer la transacción en caso de error o proceso incompleto
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            var response = _mapper.Map<OrderDto>(data);
            return response;
        }
        catch (Exception e)
        {
            throw new Exception($"Error in UpdateAsync: {e.Message}");
        }
    }
}
