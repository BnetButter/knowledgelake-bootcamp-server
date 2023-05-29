using Microsoft.AspNetCore.Mvc;
using MenuAPI.Models;
using MenuAPI.Services;

namespace MenuAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuTypesController: ControllerBase
{
    private readonly ILogger<MenuTypesController> _logger;
    public PsqlFoodTypeService FoodTypeService { get; }
    
    public MenuTypesController(
        ILogger<MenuTypesController> logger,
        PsqlFoodTypeService typeService
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