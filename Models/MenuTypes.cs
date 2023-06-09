using System.Text.Json.Serialization;
using System.Text.Json;

namespace MenuAPI.Models;

public class FoodType
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type_id")]
    public int Type_id { get; set; }

    [JsonPropertyName("visible")]
    public bool visible { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize<FoodType>(this);
    }
}