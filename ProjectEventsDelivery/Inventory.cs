namespace OrderDeliverySystem;

public class Inventory
{
    public event EventHandler<OrderEventArgs> InsufficientStock;

    public bool CheckStock(Order order)
    {
        bool stockOk = new Random().Next(0, 2) == 1;
            
        if (!stockOk)
        {
            Console.WriteLine($"[{this.GetType().Name}] Недостатньо товару для замовлення №{order.OrderId}.");
            InsufficientStock?.Invoke(this, new OrderEventArgs(order));
        }
        else
        {
            Console.WriteLine($"[{this.GetType().Name}] Товари для замовлення №{order.OrderId} в наявності.");
        }
        return stockOk;
    }
}