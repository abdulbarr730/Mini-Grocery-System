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
            new Product { Id = 2, Name = "Bread", Price = 1.5m, Stock = 5 }
        );
    }
}