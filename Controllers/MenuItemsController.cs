using Microsoft.AspNetCore.Mvc;
using MenuAPI.Models;
using MenuAPI.Services;

namespace MenuAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuItemsController: ControllerBase
{
    private readonly ILogger<MenuItemsController> _logger;
    public JsonFileFoodItemService jsonFileFoodItemService { get; }
    
    public MenuItemsController(
        ILogger<MenuItemsController> logger,
        JsonFileFoodItemService itemService
        )
    {
        _logger = logger;
        jsonFileFoodItemService = itemService;
    }

    [HttpGet(Name="GetMenuItems")]
    public IEnumerable<FoodItem> Get()
    {
        return jsonFileFoodItemService.GetFoodItems();
    }
}