using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using OrderDeliverySystem;

namespace ProjectEventsDelivery;

// Вмонтовування логіки програми в графічний інтерфейс
public partial class App : Application
{
    public static OrderManager orderManager = new OrderManager();
    public static Inventory inventory = new Inventory();
    public static PaymentProcessor paymentProcessor = new PaymentProcessor();
    public static DeliveryService deliveryService = new DeliveryService();
    public static NotificationSystem notificationSystem = new NotificationSystem();
    public static User user = new User();
    
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }
        notificationSystem.Subscribe(orderManager, paymentProcessor, deliveryService, inventory);
        user.Subscribe(deliveryService);
        
        paymentProcessor.PaymentConfirmed += (sender, e) =>
        {
            Thread.Sleep(500);
            orderManager.PrepareOrder(e.Order);
        };
            
        orderManager.OrderReadyForDelivery += (sender, e) =>
        {
            Thread.Sleep(500);
            deliveryService.StartDelivery(e.Order);
        };

        base.OnFrameworkInitializationCompleted();
    }
}