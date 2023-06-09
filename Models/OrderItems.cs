using System.Text.Json.Serialization;
using System.Text.Json;

namespace OrdersAPI.Models;


public class OrderItem
{
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; }

    [JsonPropertyName("itemId")]
    public int ItemId { get; set; }

    [JsonPropertyName("options")]
    public int[] Options { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    public override string ToString()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(this, options);
    }
}

