namespace OrderDeliverySystem;

public class PaymentProcessor
{
    public event EventHandler<OrderEventArgs> PaymentConfirmed;

    public void ProcessPayment(Order order)
    {
        Console.WriteLine($"[{this.GetType().Name}] Обробка платежу по замовленю №{order.OrderId}...");
        Thread.Sleep(500); 
        Console.WriteLine($"[{this.GetType().Name}] Ваш платіж по замовленню №{order.OrderId} підтверджено.");
        PaymentConfirmed?.Invoke(this, new OrderEventArgs(order));
    }
}