using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;
using pruebatecnica.Infrastructure.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace pruebatecnica.Infrastructure.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public OrderDetailRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDetailDto>> GetAsync()
    {
        try
        {
            var data = await _context.orderDetails.ToListAsync();
            var response = data.Select(x => _mapper.Map<OrderDetailDto>(x)).ToList();
            return response;
        }
        catch (Exception e) 
        {
            throw new Exception($"Error in GetAsync: {e.Message}");          
        }
    }

    public async Task<OrderDetailDto> GetById(int id)
    {
        try
        {
            var data = await _context.orderDetails
            .FirstOrDefaultAsync(x => x.OrderDetailId == id)
             ?? throw new Exception($"OrderDetail with Id '{id}' not found");

            var response = _mapper.Map<OrderDetailDto>(data);
            return response;
        }
        catch (Exception e)
        {
            throw new Exception($"Error in GetById: {e.Message}");
        }
    }

    public async Task<IEnumerable<OrderDetailDto>> GetByOrderId(int OrderId)
    {
        var data =  await _context.orderDetails
            .Where(od => od.OrderId == OrderId)
            .ToListAsync();

        var response = data.Select(x => _mapper.Map<OrderDetailDto>(x)).ToList();
        return response;
    }

    public async Task<OrderDetailDto> CreateAsync(OrderDetailDto orderDetail)
    {
        try
        {
            var order = await _context.orders.FindAsync(orderDetail.OrderId)
                ?? throw new Exception("Order not found");

            var product = await _context.products.FindAsync(orderDetail.ProductId)
                ?? throw new Exception("Product not found");

            var data = _mapper.Map<OrderDetail>(orderDetail);

            data.Order = order;
            data.Product = product;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.orderDetails.Add(data);
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
            var response = _mapper.Map<OrderDetailDto>(data);
            return response;
        }
        catch (Exception e)
        {
            throw new Exception($"Error in CreateAsync: {e.Message}");
        }
    }

    public async Task<OrderDetailDto> UpdateAsync(int id, OrderDetailDto newData)
    {
        try
        {
            var data = await _context.orderDetails.FindAsync(id)
                ?? throw new Exception($"OrderDetail with Id '{id}' not found");

            _mapper.Map(newData, data);

            data.Order = await _context.orders.FindAsync(newData.OrderId)
                 ?? throw new Exception("Order not found");

            data.Product = await _context.products.FindAsync(newData.ProductId)
                 ?? throw new Exception("Product not found");

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //Actualizar los datos en el objeto "data" obtenido
                    _context.orderDetails.Update(data);
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

            var response = _mapper.Map<OrderDetailDto>(data);
            return response;
        }
        catch (Exception e)
        {
            throw new Exception($"Error in UpdateAsync: {e.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var data = await _context.orderDetails.FindAsync(id)
                ?? throw new Exception($"OrderDetail with Id '{id}' not found");

            _context.orderDetails.Remove(data);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error in DeleteAsync: {e.Message}");
        }
    }

}
