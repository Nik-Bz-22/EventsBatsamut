namespace OrderDeliverySystem;

public class User
{
    private Dictionary<int, string> phrases = new Dictionary<int, string>()
    {
        { 0, "Який жахливий сервіс і сам магазин!" },
        { 1, "Менеджер — невихований хам." },
        { 2, "Дуже довго довелося чекати(" },
        { 3, "Все ніби непогано, але відчуття паршиве(" },
        { 4, "Дякую вам, все сподобалося." },
        { 5, "Вау, все супер, особливо чудове ставлення до клієнтів!" },
    };
        
    public void Subscribe(DeliveryService deliveryService)
    {
        deliveryService.OrderDelivered += (s, e) =>
        {
            int stars = new Random().Next(0, 6);
                
            Console.WriteLine();
            Console.WriteLine($"[{this.GetType().Name}] Оцінюю замовлення №{e.Order.OrderId} на {stars}/5");
            Console.WriteLine($"[{this.GetType().Name}] {phrases[stars]}");
            Console.WriteLine($"[{this.GetType().Name}] {e.Order}");
        };

        deliveryService.OrderInTransit += (s, e) =>
        {
            Console.WriteLine();
            Console.WriteLine($"[{this.GetType().Name}] Ну-ну, я чекаю своє замовлення!");
            Console.WriteLine($"[{this.GetType().Name}] Я вже так хочу отримати ({e.Order.GetItemsString()})");
        };
    }

}