using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;
using pruebatecnica.Infrastructure.Data;

namespace pruebatecnica.Infrastructure.Repositories;

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
            Console.WriteLine($"Error in GetAsync: {e.Message}");
            throw;
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
            Console.WriteLine($"Error in GetById: {e.Message}");
            throw;
        }
    }

    public async Task<ShoppingCartDto> CreateAsync(ShoppingCartDto shoppingCart)
    {
        try
        {

            var customer = await _context.customers.FindAsync(shoppingCart.CustomerId)
                     ?? throw new Exception("Order not found");

            var product = await _context.products.FindAsync(shoppingCart.ProductId)
                          ?? throw new Exception("Product not found");

            var data = _mapper.Map<ShoppingCart>(shoppingCart);

            data.Customer = customer;
            data.Product = product;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.shoppingCarts.Add(data);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            var response = _mapper.Map<ShoppingCartDto>(data);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in CreateAsync: {e.Message}");
            throw;
        }
    }

    public async Task<ShoppingCartDto> UpdateAsync(int id, ShoppingCartDto shoppingCartDto)
    {
        try
        {
            var existingShoppingCart = await _context.shoppingCarts.FindAsync(id)
                ?? throw new Exception($"ShoppingCart with Id '{id}' not found");

            _mapper.Map(shoppingCartDto, existingShoppingCart);

            existingShoppingCart.Customer = await _context.customers.FindAsync(shoppingCartDto.CustomerId)
                                     ?? throw new Exception("Order not found");

            existingShoppingCart.Product = await _context.products.FindAsync(shoppingCartDto.ProductId)
                                        ?? throw new Exception("Product not found");


            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                _context.shoppingCarts.Update(existingShoppingCart);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            var response = _mapper.Map<ShoppingCartDto>(existingShoppingCart);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in UpdateAsync: {e.Message}");
            throw;
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
            Console.WriteLine($"Error in DeleteAsync: {e.Message}");
            throw;
        }
    }
}
    
