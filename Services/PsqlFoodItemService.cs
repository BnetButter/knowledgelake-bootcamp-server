using Npgsql;
using MenuAPI.Models;
using MenuAPI.Interface;
namespace MenuAPI.Services;

public class PsqlFoodItemService: IFoodItemService
{
    NpgsqlConnection conn { get; }

    public PsqlFoodItemService()
    {
        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=root_db;";
        conn = new NpgsqlConnection(connectionString);
    }

    ~PsqlFoodItemService()
    {

    }

    public IEnumerable<FoodItem> GetFoodItems()
    {
        conn.Open();
        try {
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
        finally {
            conn.Close();
        }   
    }
}
