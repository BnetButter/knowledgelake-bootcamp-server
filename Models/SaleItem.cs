using System.Text.Json.Serialization;
using System.Text.Json;

namespace OrdersAPI.Models;

/*
  id: Number, // use MenuAPI object
  options: Number[],
  quantity: Number,
*/

public class SaleItem 
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("options")]
    public int [] Options {get; set;}
    
    [JsonPropertyName("quantity")]
    public int Quantity {get; set;}
}
