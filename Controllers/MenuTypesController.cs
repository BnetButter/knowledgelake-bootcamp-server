using Microsoft.AspNetCore.Mvc;
using MenuAPI.Models;
using MenuAPI.Services;

namespace MenuAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuTypesController: ControllerBase
{
    private readonly ILogger<MenuTypesController> _logger;
    public JsonFileFoodTypeService jsonFileFoodTypeService { get; }
    
    public MenuTypesController(
        ILogger<MenuTypesController> logger,
        JsonFileFoodTypeService typeService
        )   
    {
        _logger = logger;
        jsonFileFoodTypeService = typeService;
    }

    [HttpGet(Name="GetMenuTypes")]
    public IEnumerable<FoodType> Get()
    {
        return jsonFileFoodTypeService.GetFoodTypes();
    }
}