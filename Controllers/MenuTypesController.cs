using Microsoft.AspNetCore.Mvc;
using MenuAPI.Models;
using MenuAPI.Services;
using MenuAPI.Interface;

namespace MenuAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuTypesController: ControllerBase
{
    private readonly ILogger<MenuTypesController> _logger;
    public IFoodTypeService FoodTypeService { get; }
    
    public MenuTypesController(
        ILogger<MenuTypesController> logger,
        IFoodTypeService typeService
        )   
    {
        _logger = logger;
        FoodTypeService = typeService;
    }

    [HttpGet(Name="GetMenuTypes")]
    public IEnumerable<FoodType> Get()
    {
        return FoodTypeService.GetFoodTypes();
    }
}