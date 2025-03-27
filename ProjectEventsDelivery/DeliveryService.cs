namespace OrderDeliverySystem;

public class DeliveryService
{
    public event EventHandler<OrderEventArgs> OrderInTransit;
    public event EventHandler<OrderEventArgs> OrderDelivered;

    public void StartDelivery(Order order)
    {
        Console.WriteLine($"[{this.GetType().Name}] Замовлення №{order.OrderId} відправлене (в дорозі).");
        OrderInTransit?.Invoke(this, new OrderEventArgs(order));


        Thread.Sleep(500);
        Console.WriteLine($"[{this.GetType().Name}] Замовлення №{order.OrderId} доставлено.");
        OrderDelivered?.Invoke(this, new OrderEventArgs(order));
    }

    private void WaitForEnter(string prompt)
    {
        Console.WriteLine(prompt);
        Console.ReadLine();
    }
}