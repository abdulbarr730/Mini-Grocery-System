using Microsoft.AspNetCore.Mvc;
using GroceryAPI.Repositories; 
using GroceryAPI.Services;     
using GroceryAPI.Models;

namespace GroceryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
  
    private readonly IProductRepository _productRepo; 

    
    public OrdersController(IOrderService orderService, IProductRepository productRepo)
    {
        _orderService = orderService;
        _productRepo = productRepo;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        
        var products = await _productRepo.GetAllAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest request)
    {
        var result = await _orderService.PlaceOrderAsync(request.ProductId, request.Quantity);
        if (!result.Success) return BadRequest(new { message = result.Message });
        return Ok(new { message = result.Message });
    }
}

public record OrderRequest(int ProductId, int Quantity);