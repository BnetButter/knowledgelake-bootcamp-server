using Microsoft.AspNetCore.Mvc;
using MenuAPI.Models;
using MenuAPI.Services;
using MenuAPI.Interface;

namespace MenuAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuItemsController: ControllerBase
{
    private readonly ILogger<MenuItemsController> _logger;
    public IFoodItemService FoodItemService { get; }  // Change to an interface
    
    public MenuItemsController(
        ILogger<MenuItemsController> logger,
        IFoodItemService itemService
        )
    {
        _logger = logger;
        FoodItemService = itemService;
    }

    [HttpGet(Name="GetMenuItems")]
    public IEnumerable<FoodItem> Get()
    {
        return FoodItemService.GetFoodItems();
    }
}