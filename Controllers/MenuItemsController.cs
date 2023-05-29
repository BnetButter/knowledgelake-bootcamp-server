using Microsoft.AspNetCore.Mvc;
using MenuAPI.Models;
using MenuAPI.Services;

namespace MenuAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuItemsController: ControllerBase
{
    private readonly ILogger<MenuItemsController> _logger;
    public PsqlFoodItemService FoodItemService { get; }
    
    public MenuItemsController(
        ILogger<MenuItemsController> logger,
        PsqlFoodItemService itemService
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