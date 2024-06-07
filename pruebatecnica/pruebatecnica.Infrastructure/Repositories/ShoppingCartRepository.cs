using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;
using pruebatecnica.Infrastructure.Data;

namespace pruebatecnica.Infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ShoppingCartRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShoppingCartDto>> GetAsync()
        {
            try
            {
                var data = await _context.shoppingCarts.ToListAsync();
                var response = data.Select(x => _mapper.Map<ShoppingCartDto>(x)).ToList();
                return response;
            }
            catch (Exception e)
            {
                throw new Exception($"Error in GetAsync: {e.Message}");
            }
        }

        public async Task<ShoppingCartDto> GetById(int id)
        {
            try
            {
                var data = await _context.shoppingCarts
                .FirstOrDefaultAsync(x => x.ShoppingCartId == id)
                 ?? throw new Exception($"ShoppingCart with Id '{id}' not found");

                var response = _mapper.Map<ShoppingCartDto>(data);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception($"Error in GetById: {e.Message}");
            }
        }

        public async Task<ShoppingCartDto> CreateAsync(ShoppingCartDto shoppingCartDto)
        {
            try
            {
                var customer = await _context.customers.FindAsync(shoppingCartDto.CustomerId)
                    ?? throw new Exception("Customer not found");

                var product = await _context.products.FindAsync(shoppingCartDto.ProductId)
                    ?? throw new Exception("Product not found");

                var data = _mapper.Map<ShoppingCart>(shoppingCartDto);

                data.Customer = customer;
                data.Product = product;

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.shoppingCarts.Add(data);
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
                var response = _mapper.Map<ShoppingCartDto>(data);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception($"Error in CreateAsync: {e.Message}");
            }
        }

        public async Task<ShoppingCartDto> UpdateAsync(int id, ShoppingCartDto newData)
        {
            try
            {
                var data = await _context.shoppingCarts.FindAsync(id)
                    ?? throw new Exception($"ShoppingCart with Id '{id}' not found");

                _mapper.Map(newData, data);

                data.Customer = await _context.customers.FindAsync(newData.CustomerId)
                     ?? throw new Exception("Customer not found");

                data.Product = await _context.products.FindAsync(newData.ProductId)
                     ?? throw new Exception("Product not found");

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        //Actualizar los datos en el objeto "data" obtenido
                        _context.shoppingCarts.Update(data);
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

                var response = _mapper.Map<ShoppingCartDto>(data);
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
                var data = await _context.shoppingCarts.FindAsync(id)
                    ?? throw new Exception($"ShoppingCart with Id '{id}' not found");

                _context.shoppingCarts.Remove(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Error in DeleteAsync: {e.Message}");
            }
        }

        public async Task<IEnumerable<ShoppingCartDto>> GetByCustomerId(int customerId)
        {
            try
            {
                var data = await _context.shoppingCarts
                    .Where(x => x.CustomerId == customerId)
                    .ToListAsync();

                var response = data.Select(x => _mapper.Map<ShoppingCartDto>(x)).ToList();
                return response;
            }
            catch (Exception e)
            {
                throw new Exception($"Error in GetByCustomerIdAsync: {e.Message}");
            }
        }

    }
}
