using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Models;
using OrdersAPI.Services;
namespace OrdersAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    
    public PsqlOrderItemService Service;

    public OrdersController(
        ILogger<OrdersController> logger,
        PsqlOrderItemService service
        )
    {
        Service = service;
    }

    [HttpGet(Name = "GetOrderItem")]
    public IEnumerable<OrderItem> Get(int orderId, bool completed)
    {
        return Service.GetOrderItems(orderId);
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
