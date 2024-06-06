using Microsoft.EntityFrameworkCore;
using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }
        public DbSet<Payment> payments { get; set; }
    }
}
