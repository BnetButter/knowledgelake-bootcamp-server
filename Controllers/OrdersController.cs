using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Models;
using OrdersAPI.Services;
using OrdersAPI.Interface;
namespace OrdersAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    
    public IOrderItemService Service;

    public OrdersController(
        ILogger<OrdersController> logger,
        IOrderItemService service
        )
    {
        Service = service;
    }

    [HttpGet(Name = "GetOrderItem")]
    public IEnumerable<OrderItem> Get()
    {
        return Service.GetPendingOrderItems();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderItem[] orderItems)
    {
        // Here you can implement your logic to handle the POST request.
        // In this case, we assume that the request body will be serialized into an instance of MyModel.

        // Just for an example, we assume that MyModel has a single property named Text.

        Service.AddOrderItems(orderItems);

        // If the Text property of the received model is valid, return a success response.
        return Ok($"Received model with text");
    }
}
