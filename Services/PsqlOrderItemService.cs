using Npgsql;
using OrdersAPI.Models;
using OrdersAPI.Interface;
namespace OrdersAPI.Services;

public class PsqlOrderItemService: IOrderItemService 
{
    NpgsqlConnection conn { get; }

    public PsqlOrderItemService()
    {
        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=root_db;";
        conn = new NpgsqlConnection(connectionString);
    }
    
        
    public IEnumerable<OrderItem> GetPendingOrderItems()
    {
        var orderItems = new List<OrderItem>();
        
        using (NpgsqlCommand cmd = new NpgsqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT \"OrderID\", \"ItemID\", \"Options\", \"Status\" FROM public.\"nc_hzs9___Orders\" WHERE \"Status\" = @status";
            cmd.Parameters.AddWithValue("status", "pending");

            try
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {   
                    while (reader.Read())
                    {

                        var optionsStr = reader.GetString(2);
                        int[] optionsArray = string.IsNullOrEmpty(optionsStr) ? new int[0] : optionsStr.Split(',').Select(int.Parse).ToArray();
                        var orderItem = new OrderItem
                        {
                            OrderId = reader.GetString(0),
                            ItemId = reader.GetInt32(1),
                            Options = optionsArray,
                            Status = reader.GetString(3)
                        };

                        orderItems.Add(orderItem);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        return orderItems;
    }

    public IEnumerable<OrderItem> GetOrderItems(string OrderId)
    {
        return new OrderItem[0];
    }
    public void AddOrderItems(IEnumerable<OrderItem> items)
    {
        using (NpgsqlCommand cmd = new NpgsqlCommand())
        {
            cmd.Connection = conn;

            try
            {
                conn.Open();

                foreach (var item in items)
                {
                    cmd.CommandText = $"INSERT INTO public.\"nc_hzs9___Orders\" (\"OrderID\", \"ItemID\", \"Options\", \"Status\") VALUES (@orderId, @itemId, @options, @status)";
                    
                    cmd.Parameters.AddWithValue("orderId", item.OrderId);
                    cmd.Parameters.AddWithValue("itemId", item.ItemId);

                    // Convert the 'Options' array to a comma-separated string
                    var optionsString = string.Join(",", item.Options);
                    cmd.Parameters.AddWithValue("options", optionsString);

                    cmd.Parameters.AddWithValue("status", item.Status);

                    cmd.ExecuteNonQuery();

                    // Clear the parameters after executing the command
                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }

}
