using System.Text.Json;
using MenuAPI.Models;
namespace MenuAPI.Services;

public class JsonFileFoodTypeService
{
    public IWebHostEnvironment WebHostEnvironment { get; }

    public JsonFileFoodTypeService(IWebHostEnvironment webHostEnvironment)
    {
        WebHostEnvironment = webHostEnvironment;
    }

    private string JsonFileName {
        get
        {
            /*
                WebHostEnvironment.WebHostPath requires wwwroot folder
            */
            var path = Path.Combine("./", "data", "menutypes.json");
            return path;
        }
    }

    public IEnumerable<FoodType> GetFoodTypes()
    {
        using (var jsonFileReader = File.OpenText(JsonFileName))
        {
            return JsonSerializer.Deserialize<FoodType[]>(
                
                jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true 
                }
            );
        }
    }
}

