using DisorderedOrdersMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DisorderedOrdersMVC.DataAccess
{
    public class DisorderedOrdersContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DisorderedOrdersContext(DbContextOptions<DisorderedOrdersContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasData(
                new Customer() { Id = 1, Email = "megan@test.com", IsPreferred = true},
                new Customer() { Id = 2, Email = "molly@test.com", IsPreferred = false});

            builder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Hot Dog", StockQuantity = 100, Price = 100 },
                new Product() { Id = 2, Name = "Not a HotDog", StockQuantity = 50, Price = 250 },
                new Product() { Id = 3, Name = "Water Bottle", StockQuantity = 75 ,Price = 500},
                new Product() { Id = 4, Name = "Soda", StockQuantity = 200, Price = 325 });

        }
    }
}
