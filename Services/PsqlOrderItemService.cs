using Npgsql;
using OrdersAPI.Models;

namespace OrdersAPI.Services;

public class PsqlOrderItemService 
{
    NpgsqlConnection conn { get; }

    public PsqlOrderItemService()
    {
        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=root_db;";
        conn = new NpgsqlConnection(connectionString);
    }
    
    public IEnumerable<OrderItem> GetOrderItems(int OrderId)
    {
        return new OrderItem[0];
    }
    public void AddOrderItems(IEnumerable<OrderItem> items)
    {

    }
}
