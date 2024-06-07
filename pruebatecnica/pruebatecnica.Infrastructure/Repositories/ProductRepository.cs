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
            throw new Exception($"Error in GetAsync: {e.Message}");
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
            throw new Exception($"Error in GetById: {e.Message}");
        }
    }

    public async Task<Product> CreateAsync(Product product)
    {
        try
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.products.Add(product);
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
            return product;
        }
        catch (Exception e)
        {
            throw new Exception($"Error in CreateAsync: {e.Message}");
        }
    }

    public async Task<Product> UpdateAsync(int id, Product newData)
    {
        try
        {
            var data = await _context.products.FindAsync(id)
                    ?? throw new Exception($"Product with Id '{id}' not found");

            data.Category = newData.Category;
            data.UnitPrice = newData.UnitPrice;
            data.Stock = newData.Stock;
            data.Description = newData.Description;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.products.Update(data);
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
            return data;
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
            var product = await _context.products.FindAsync(id)
                    ?? throw new Exception($"Product with Id '{id}' not found");

            _context.products.Remove(product);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error in DeleteAsync: {e.Message}");
        }
    }
}
