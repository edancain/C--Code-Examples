using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly HttpClient _httpClient;
    
    public OrderController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ProductService");
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        // Validate stock with Product Service
        foreach (var item in order.Items)
        {
            var response = await _httpClient.GetAsync($"/api/products/{item.ProductId}/stock");
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Product {item.ProductId} is out of stock");
            }
        }
        
        // Save order logic here
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    [HttpGet("{id}")]
    public ActionResult<Order> GetOrder(int id)
    {
        // Get order logic here
        return Ok(new Order { Id = id });
    }
}