using System.Text.Json;
using MenuAPI.Models;
namespace MenuAPI.Services;

public class JsonFileFoodItemService
{
    public IWebHostEnvironment WebHostEnvironment { get; }

    public JsonFileFoodItemService(IWebHostEnvironment webHostEnvironment)
    {
        WebHostEnvironment = webHostEnvironment;
    }

    private string JsonFileName {
        get
        {
            /*
                WebHostEnvironment.WebHostPath requires wwwroot folder
            */
            var path = Path.Combine("./", "data", "menuitems.json");
            return path;
        }
    }

    public IEnumerable<FoodItem> GetFoodItems()
    {
        using (var jsonFileReader = File.OpenText(JsonFileName))
        {
            return JsonSerializer.Deserialize<FoodItem[]>(
                
                jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true 
                }
            );
        }
    }
}

