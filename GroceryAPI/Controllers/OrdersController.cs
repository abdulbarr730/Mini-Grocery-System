[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase {
    private readonly IOrderService _service;
    public OrdersController(IOrderService service) => _service = service;

    [HttpGet("products")]
    public async Task<IActionResult> Get() => Ok(await _context.Products.ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderRequest req) {
        var res = await _service.PlaceOrderAsync(req.ProductId, req.Quantity);
        return res.Success ? Ok(res) : BadRequest(res);
    }
}