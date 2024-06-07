using Microsoft.EntityFrameworkCore;
using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.Entities;
using pruebatecnica.Infrastructure.Data;

namespace pruebatecnica.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAsync()
    {
        try
        {
            return await _context.products.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in GetAsync: {e.Message}");
            throw;
        }
    }

    public async Task<Product> GetById(int id)
    {
        try
        {
            return await _context.products.FindAsync(id)
                ?? throw new Exception($"Product with Id '{id}' not found");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in GetById: {e.Message}");
            throw;
        }
    }

    public async Task<Product> CreateAsync(Product product)
    {
        try
        {
            _context.products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in CreateAsync: {e.Message}");
            throw;
        }
    }

    public async Task<Product> UpdateAsync(int id, Product product)
    {
        try
        {
            var existingProduct = await _context.products.FindAsync(id)
                ?? throw new Exception($"Product with Id '{id}' not found");

            existingProduct.Category = product.Category;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.Stock = product.Stock;
            existingProduct.Description = product.Description;

            await _context.SaveChangesAsync();
            return existingProduct;
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
            var product = await _context.products.FindAsync(id)
                    ?? throw new Exception($"Product with Id '{id}' not found");

            _context.products.Remove(product);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in DeleteAsync: {e.Message}");
            throw;
        }
    }
}
