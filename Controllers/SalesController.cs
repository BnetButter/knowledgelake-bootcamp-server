using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Models;
using OrdersAPI.Services;
using System;
using Stripe;
using Stripe.Checkout;

using MenuAPI.Services;
using MenuAPI.Models;
namespace OrdersAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesController : ControllerBase
{
    private readonly ILogger<SalesController> _logger;
    
    public PsqlFoodItemService FoodItemService;
    public SalesController(
        ILogger<SalesController> logger,
        PsqlFoodItemService foodItemService
        )
    {
        _logger = logger;
        FoodItemService = foodItemService;
        StripeConfiguration.ApiKey = "sk_test_51NDySuJhruUG4Ea9woO5MOWjSkdDoCYfd72dstSJRXfPveSOqh8D9UbVuDeAUPLPGX5qtL98HFAWQXRm4QPId51g00Pz5RimR5";
    }
    // https://stripe.com/docs/api/checkout/sessions/create

    [HttpGet(Name = "GetSales")]
    public RedirectResult Get()
    {
        var options = new SessionCreateOptions
        {
            SuccessUrl = "http://localhost:1422",
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "TestItem",
                        },
                        Currency = "USD",
                        UnitAmount = 200,
                    },
                    Quantity = 2,
                },
            },
            Mode = "payment",
        };
        var service = new SessionService();
        
        var session = service.Create(options);
        
        return Redirect(session.Url);
    }
    
    [HttpPost(Name="PostSale")]
    public IActionResult PostSale([FromBody] SaleItem[] items)
    {
        // Grab reference to menu
        List<FoodItem> menu_reference = FoodItemService.GetFoodItems();

        // Create Line Item based on sale items
        var LineItems = new List<SessionLineItemOptions>();
        for (int i = 0; i < items.Length; i++)
        {
            SaleItem saleItem = items[i];
            FoodItem foodItem = menu_reference[saleItem.Id];

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

        var options = new SessionCreateOptions
        {
            SuccessUrl = "http://localhost:1422",
            LineItems = LineItems,
            Mode = "payment",
        };
        var service = new SessionService();
        var session = service.Create(options);

        return Ok( new { redirectUrl = session.Url });
    }
}