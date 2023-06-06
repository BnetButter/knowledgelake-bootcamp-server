using Npgsql;
using MenuAPI.Models;
using MenuAPI.Interface;

namespace MenuAPI.Services;

public class PsqlFoodTypeService: IFoodTypeService
{
    NpgsqlConnection conn { get; }

    public PsqlFoodTypeService()
    {
        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=root_db;";
        conn = new NpgsqlConnection(connectionString);
    }


    public IEnumerable<FoodType> GetFoodTypes()
    {
        conn.Open();
        
        try {
            var sql = "SELECT * FROM public.\"nc_hzs9___ItemType\" ORDER BY id ASC";
            var cmd = new NpgsqlCommand(sql, conn);
            List<FoodType> items = new List<FoodType>();

            using (var rdr = cmd.ExecuteReader())
            {
                var index = 0;
                while (rdr.Read())
                {
                    items.Add(new FoodType {
                        Id = index,
                        Name = (string) rdr["name"],
                        visible = Convert.ToBoolean(rdr["visible"]),
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
