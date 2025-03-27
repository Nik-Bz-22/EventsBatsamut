namespace OrderDeliverySystem;

public class OrderManager
{
    public event EventHandler<OrderEventArgs> OrderPlaced;
    public event EventHandler<OrderEventArgs> OrderReadyForDelivery;

    public void PlaceOrder(Order order)
    {
        Console.WriteLine($"[{this.GetType().Name}] Замовленя №{order.OrderId} розміщене.");
        OrderPlaced?.Invoke(this, new OrderEventArgs(order));
    }

    public void PrepareOrder(Order order)
    {
        Console.WriteLine($"[{this.GetType().Name}] Замовленя №{order.OrderId} готове до доставки.");
        OrderReadyForDelivery?.Invoke(this, new OrderEventArgs(order));
    }
}