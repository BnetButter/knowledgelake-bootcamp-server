using System.Text.Json.Serialization;
using System.Text.Json;


namespace MenuAPI.Models;


public class FoodItem
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type_id")]
    public int Type_id { get; set; }

    [JsonPropertyName("price")]
    public int Price { get; set; }

    [JsonPropertyName("options")]
    public int[] Options { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize<FoodItem>(this);
    }

}
