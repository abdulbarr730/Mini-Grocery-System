using GroceryAPI.Data;
using GroceryAPI.Models;
using GroceryAPI.Repositories;

namespace GroceryAPI.Services;

// The Interface acts as the contract for the Controller
public interface IOrderService 
{
    Task<(bool Success, string Message)> PlaceOrderAsync(int productId, int quantity);
}

// The Implementation contains the core business logic
public class OrderService : IOrderService 
{
    private readonly AppDbContext _context;
    private readonly IProductRepository _repo;

    public OrderService(AppDbContext context, IProductRepository repo) 
    {
        _context = context;
        _repo = repo;
    }

    public async Task<(bool Success, string Message)> PlaceOrderAsync(int productId, int quantity) 
    {
        // 1. START THE SINGLE TRANSACTION (Mandatory Requirement)
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try 
        {
            // 2. Fetch the product using the Repository
            var product = await _repo.GetByIdAsync(productId);
            
            // No stock = No order
            if (product == null) return (false, "Product not found.");
            if (product.Stock < quantity) return (false, $"Insufficient stock. Only {product.Stock} left.");

            // 4. Update the stock quantity
            product.Stock -= quantity;
            _repo.Update(product);

            // 5. Create the order record
            var order = new Order {
                ProductId = productId,
                Quantity = quantity,
                TotalPrice = product.Price * quantity,
                CreatedAt = DateTime.UtcNow
            };
            await _repo.AddOrderAsync(order);

            // 6. Save all changes to the database
            await _context.SaveChangesAsync();
            
            // 7. COMMIT THE TRANSACTION: Everything succeeds together
            await transaction.CommitAsync();

            return (true, "Order placed successfully!");
        }
        catch (Exception ex) 
        {
            // 8. ROLLBACK: If any step fails, nothing is saved to the DB
            await transaction.RollbackAsync();
            return (false, "A database error occurred during the transaction.");
        }
    }
}