using Npgsql;
using MenuAPI.Models;
using System.Text.Json;


namespace MenuAPI.Services;

public class PsqlFoodItemService 
{

    NpgsqlConnection conn { get; }

    public PsqlFoodItemService()
    {
        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=root_db;";
        conn = new NpgsqlConnection(connectionString);
        conn.Open();
    }

    public IEnumerable<FoodItem> GetFoodItems()
    {
        
        var sql = "SELECT * FROM public.\"nc_hzs9___FoodItem\" ORDER BY id ASC";

        var cmd = new NpgsqlCommand(sql, conn);


        List<FoodItem> items = new List<FoodItem>();

        using (var rdr = cmd.ExecuteReader())
        {
    
            var index = 0;
            while (rdr.Read())
            {
                items.Add(new FoodItem {
                    Id = index,
                    Name = (string) rdr["name"],
                    Price = Convert.ToInt32((Int64) rdr["price"]),
                    Type_id = Convert.ToInt32((Int64) rdr["type_id"]),
                    Options = rdr["options"] != System.DBNull.Value ? 
                        ((string) rdr["options"]).Split(",").Select(int.Parse).ToArray() :
                        new int[0],
                });
                index += 1;
            }
        }
        return items;

    }
}
