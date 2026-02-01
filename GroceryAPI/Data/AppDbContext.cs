using Microsoft.EntityFrameworkCore;
using GroceryAPI.Models;

namespace GroceryAPI.Data;
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Milk", Price = 2.5m, Stock = 10 },
        new Product { Id = 2, Name = "Bread", Price = 1.5m, Stock = 5 },
        new Product { Id = 3, Name = "Eggs (Dozen)", Price = 3.99m, Stock = 15 },
        new Product { Id = 4, Name = "Apple", Price = 0.5m, Stock = 50 },
        new Product { Id = 5, Name = "Cheese", Price = 4.5m, Stock = 8 }
        );
    }
}