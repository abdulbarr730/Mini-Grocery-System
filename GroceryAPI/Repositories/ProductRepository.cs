using GroceryAPI.Data;
using GroceryAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace GroceryAPI.Repositories;

public interface IProductRepository {
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    void Update(Product product);
    Task AddOrderAsync(Order order);
}

public class ProductRepository : IProductRepository {
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context) => _context = context;
    public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();
    public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);
    public void Update(Product product) => _context.Products.Update(product);
    public async Task AddOrderAsync(Order order) => await _context.Orders.AddAsync(order);
}