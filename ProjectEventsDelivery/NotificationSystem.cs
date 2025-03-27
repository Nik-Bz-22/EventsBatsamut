namespace OrderDeliverySystem;

public class NotificationSystem
{
    public void Subscribe(OrderManager orderManager, PaymentProcessor paymentProcessor, DeliveryService deliveryService, Inventory inventory)
    {
        orderManager.OrderPlaced += (s, e) =>
        {
            Console.WriteLine($"\n[{this.GetType().Name}] Нове замовлення №{e.Order.OrderId} розміщено.");
        };
        inventory.InsufficientStock += (s, e) =>
        {
            Console.WriteLine($"\n[{this.GetType().Name}] Замовлення №{e.Order.OrderId} відхилене через нестачу товару.");
        };
        paymentProcessor.PaymentConfirmed += (s, e) =>
        {
            Console.WriteLine($"\n[{this.GetType().Name}] Платіж за замовленням №{e.Order.OrderId} підтверджено.");
        };
        orderManager.OrderReadyForDelivery += (s, e) =>
        {
            Console.WriteLine($"\n[{this.GetType().Name}] Замовлення №{e.Order.OrderId} готове до доставки.");
        };
        deliveryService.OrderInTransit += (s, e) =>
        {
            Console.WriteLine($"\n[{this.GetType().Name}] Замовлення №{e.Order.OrderId} в дорозі.");
        };
        deliveryService.OrderDelivered += (s, e) =>
        {
            Console.WriteLine($"\n[{this.GetType().Name}] Замовлення №{e.Order.OrderId} доставлено за адресою {e.Order.DeliveryAddress}");
        };
    }
}