using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using OrderDeliverySystem;

namespace ProjectEventsDelivery
{
    public class MainWindow : Window
    {
        private ListBox productListBox;
        private ListBox orderListBox;
        private ComboBox categoryComboBox;
        private TextBox addressTextBox;
        private Border confirmationOverlay;
        private Border errorOverlay;

        private Dictionary<Product, int> orderItems = new Dictionary<Product, int>();

        public MainWindow()
        {
            InitializeComponent();

            productListBox = this.FindControl<ListBox>("productListBox");
            orderListBox = this.FindControl<ListBox>("orderListBox");
            categoryComboBox = this.FindControl<ComboBox>("categoryComboBox");
            addressTextBox = this.FindControl<TextBox>("addressTextBox");
            confirmationOverlay = this.FindControl<Border>("confirmationOverlay");
            errorOverlay = this.FindControl<Border>("errorOverlay");

            LoadCategoriesFromJson("Static/goods.json");
            App.deliveryService.OrderDelivered += OnOrderDelivered;
        }

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

        private void LoadCategoriesFromJson(string filePath)
        {
            var json = System.IO.File.ReadAllText(filePath);
            var categories = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Category>>(json);

            categoryComboBox.ItemsSource = categories.Select(c => c.Name).ToList();
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoryComboBox.SelectedItem is string selectedCategory)
            {
                LoadProductsForCategory(selectedCategory);
            }
        }

        private void LoadProductsForCategory(string categoryName)
        {
            var json = System.IO.File.ReadAllText("Static/goods.json");
            var categories = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Category>>(json);
            var selectedCategory = categories.FirstOrDefault(c => c.Name == categoryName);

            if (selectedCategory != null)
            {
                productListBox.ItemsSource = selectedCategory.Products;
            }
        }

        private void ProductListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {}

        private void AddToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (productListBox.SelectedItem is Product selectedProduct)
            {
                if (orderItems.ContainsKey(selectedProduct))
                {
                    orderItems[selectedProduct]++;
                }
                else
                {
                    orderItems[selectedProduct] = 1;
                }

                UpdateOrderListBox();
            }
        }

        private void MakeOrder(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            foreach (var pair in orderItems)
            {
                order += pair;
            }

            order.DeliveryAddress = addressTextBox.Text;
            App.orderManager.PlaceOrder(order);
            bool stockOk = App.inventory.CheckStock(order);
            
            if (stockOk)
            {
                Thread.Sleep(100);
                App.paymentProcessor.ProcessPayment(order);
            }
            else
            {
                ShowErrorOverlay();
                return;
            }
        }

        private void UpdateOrderListBox()
        {
            var orderDisplay = orderItems.Select(item => $"{item.Key} x {item.Value}").ToList();
            orderListBox.ItemsSource = orderDisplay;
        }

        private void OnOrderDelivered(object? sender, EventArgs e)
        {
            ShowDeliveryConfirmation();
        }

        private async void ShowDeliveryConfirmation()
        {
            confirmationOverlay.Opacity = 1;
            confirmationOverlay.IsVisible = true;

            await Task.Delay(1000);

            confirmationOverlay.Opacity = 0;
            confirmationOverlay.IsVisible = false;
        }

        private async void ShowErrorOverlay()
        {
            await Task.Delay(1000);

            errorOverlay.Opacity = 1;
            errorOverlay.IsVisible = true;

            await Task.Delay(1000);

            errorOverlay.Opacity = 0;
            errorOverlay.IsVisible = false;
        }
    }

    public class Category
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Price}";
        }
    }
}
