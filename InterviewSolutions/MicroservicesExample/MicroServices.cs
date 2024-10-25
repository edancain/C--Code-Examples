// OrderService/Models/Order.cs
public class Order
{
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> Items { get; set; }
}

public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

// OrderService/Controllers/OrderController.cs
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

// ProductService/Models/Product.cs
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

// ProductService/Controllers/ProductController.cs
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        // Get product logic here
        return Ok(new Product { Id = id });
    }

    [HttpGet("{id}/stock")]
    public ActionResult<int> GetStock(int id)
    {
        // Get stock logic here
        return Ok(10);
    }
}

// Gateway/ocelot.json
{
    "Routes": [
        {
            "DownstreamPathTemplate": "/api/orders/{everything}",
            "DownstreamScheme": "http",
            "DownstreamHostAndPorts": [
                {
                    "Host": "orderservice",
                    "Port": 80
                }
            ],
            "UpstreamPathTemplate": "/gateway/orders/{everything}",
            "UpstreamHttpMethod": [ "GET", "POST" ]
        },
        {
            "DownstreamPathTemplate": "/api/products/{everything}",
            "DownstreamScheme": "http",
            "DownstreamHostAndPorts": [
                {
                    "Host": "productservice",
                    "Port": 80
                }
            ],
            "UpstreamPathTemplate": "/gateway/products/{everything}",
            "UpstreamHttpMethod": [ "GET" ]
        }
    ]
}

// docker-compose.yml
version: '3.8'
services:
  gateway:
    build:
      context: ./Gateway
    ports:
      - "5000:80"
    depends_on:
      - orderservice
      - productservice

  orderservice:
    build:
      context: ./OrderService
    ports:
      - "5001:80"

  productservice:
    build:
      context: ./ProductService
    ports:
      - "5002:80"