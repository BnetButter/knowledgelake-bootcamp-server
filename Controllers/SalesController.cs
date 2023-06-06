using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Models;
using OrdersAPI.Services;
using System;
using Stripe;
using Stripe.Checkout;
using System.Linq;


using MenuAPI.Services;
using MenuAPI.Models;
using MenuAPI.Interface;

using OrdersAPI.Models;
using OrdersAPI.Interface;
namespace OrdersAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesController : ControllerBase
{
    private readonly ILogger<SalesController> _logger;
    
    public IFoodItemService FoodItemService;
    public IOrderItemService OrderItemService;
    public SalesController(
        ILogger<SalesController> logger,
        IFoodItemService foodItemService,
        IOrderItemService orderItemService
        )
    {
        _logger = logger;
        FoodItemService = foodItemService;
        OrderItemService = orderItemService;
        StripeConfiguration.ApiKey = "sk_test_51NDySuJhruUG4Ea9woO5MOWjSkdDoCYfd72dstSJRXfPveSOqh8D9UbVuDeAUPLPGX5qtL98HFAWQXRm4QPId51g00Pz5RimR5";
    }
    // https://stripe.com/docs/api/checkout/sessions/create

    [HttpGet(Name = "GetSales")]
    public void Get()
    {

    }
    
    [HttpPost(Name="PostSale")]
    public IActionResult PostSale([FromBody] SaleItem[] items)
    {
        // Grab reference to menu
        List<FoodItem> menu_reference = new List<FoodItem>();
        
        foreach (var item in FoodItemService.GetFoodItems()) {
            menu_reference.Add(item);
        }

        var orderItems = new OrderItem[items.Length];
        var uuid = Guid.NewGuid().ToString();

        // Create Line Item based on sale items
        var LineItems = new List<SessionLineItemOptions>();
        for (int i = 0; i < items.Length; i++)
        {
            SaleItem saleItem = items[i];
            FoodItem foodItem = menu_reference[saleItem.Id];
            orderItems[i] = new OrderItem {
                OrderId = uuid,
                Options = saleItem.Options,
                ItemId = saleItem.Id,
                Status = "pending", // should be posted, but makes it easier to test
            };
       
            var lineItemOption = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = foodItem.Name,
                    },
                    Currency = "USD",
                    UnitAmount = foodItem.Price,
                },
                Quantity = saleItem.Quantity
            };

            LineItems.Add(lineItemOption);

        }

        OrderItemService.AddOrderItems(orderItems);

        var options = new SessionCreateOptions
        {
            SuccessUrl = "http://localhost:1422/Sales?<unique-id>",
            LineItems = LineItems,
            Mode = "payment",
        };
        var service = new SessionService();
        var session = service.Create(options);

        return Ok( new { redirectUrl = session.Url });
    }
}