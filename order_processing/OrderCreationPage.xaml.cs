using order_processing.Models;
using System;
using System.Linq;
using System.Windows;

namespace order_processing
{
    public partial class OrderCreationPage : Window
    {
        private MyDBContext _context;

        public OrderCreationPage()
        {
            InitializeComponent();
            _context = new MyDBContext();
            LoadData();
        }

        private void LoadData()
        {
            // Загрузка товаров
            var products = _context.Products.ToList();
            ProductComboBox.ItemsSource = products;

            // Загрузка заказчиков
            var suppliers = _context.Suppliers.ToList();
            SupplierComboBox.ItemsSource = suppliers;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, выбраны ли товар и заказчик
            if (ProductComboBox.SelectedValue == null || SupplierComboBox.SelectedValue == null)
            {
                MessageBox.Show("Пожалуйста, выберите товар и заказчика.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка количества
            if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
            {
                var selectedProductId = (int)ProductComboBox.SelectedValue;
                var selectedSupplierId = (int)SupplierComboBox.SelectedValue;

                var product = _context.Products.Find(selectedProductId);
                if (product != null)
                {
                    // Создание нового заказа
                    var order = new Order
                    {
                        CreationDate = DateTime.Now,
                        Status = "ожидает оплаты",
                        SupplierId = selectedSupplierId
                    };

                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    // Добавление товара в заказ
                    var productHasOrder = new ProductHasOrder
                    {
                        ProductId = selectedProductId,
                        OrderId = order.OrderId,
                        Quantity = quantity
                    };

                    _context.ProductHasOrders.Add(productHasOrder);
                    _context.SaveChanges();

                    // Сообщение об успешном оформлении заказа
                    MessageBox.Show($"Заказ оформлен на сумму {product.Price * quantity} ₽", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Закрытие окна создания заказа
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Введите корректное количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие окна создания заказа
            this.Close();
        }
    }
}