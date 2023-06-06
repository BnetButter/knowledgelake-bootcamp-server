namespace OrdersAPI.Interface;

using OrdersAPI.Models;

public interface IOrderItemService {


    public IEnumerable<OrderItem> GetPendingOrderItems();

    public IEnumerable<OrderItem> GetOrderItems(string OrderId);
    public void AddOrderItems(IEnumerable<OrderItem> items);
}