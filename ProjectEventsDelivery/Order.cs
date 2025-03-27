using ProjectEventsDelivery;

namespace OrderDeliverySystem;

public class Order
{
    public Guid OrderId { get; set; }
    public Dictionary<Product, int> Items { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; }

    public string GetItemsString()
    {
        return string.Join(", ", Items.Select(i => $"{i.Key.Name} x{i.Value}"));
    }

    public override string ToString()
    {
        return $"OrderId: {OrderId}, DeliveryAddress: {DeliveryAddress}, TotalAmount: {TotalAmount}, Items: {GetItemsString()}";
    }

    public Order(Dictionary<Product, int> items, string deliveryAddress)
    {
        Items = new Dictionary<Product, int>(items);
        TotalAmount = items.Sum(i => i.Key.Price * i.Value);
        DeliveryAddress = deliveryAddress;
        OrderId = Guid.NewGuid();
    }

    public Order()
    {
        Items = new Dictionary<Product, int>();
        TotalAmount = 0;
        DeliveryAddress = "";
        OrderId = Guid.NewGuid();
    }
    
    // Перевантаження оператора +
    public static Order operator +(Order order, KeyValuePair<Product, int> product)
    {
        var newItems = new Dictionary<Product, int>(order.Items);

        if (newItems.ContainsKey(product.Key))
        {
            newItems[product.Key] += product.Value; // Збільшуємо кількість 
        }
        else
        {
            newItems[product.Key] = product.Value; // Додаємо новий товар
        }

        return new Order(newItems, order.DeliveryAddress);
    }
}

public class OrderEventArgs : EventArgs
{
    public Order Order { get; }
    public OrderEventArgs(Order order)
    {
        Order = order;
    }
}