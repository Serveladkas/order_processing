using order_processing.Models;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace order_processing
{
    public partial class OrderEditPage : Window
    {
        private Order _order; // Хранит текущий заказ для редактирования

        public OrderEditPage(Order order)
        {
            InitializeComponent();
            _order = order; // Получаем существующий заказ
            LoadSuppliers(); // Загружаем поставщиков в ComboBox
            DataContext = _order; // Устанавливаем контекст данных

            // Устанавливаем значения для редактирования
            QuantityTextBox.Text = _order.ProductHasOrders.FirstOrDefault()?.Quantity.ToString();
            SupplierComboBox.SelectedValue = _order.SupplierId; // Устанавливаем текущего поставщика

            // Загружаем название товара
            LoadProductTitle(); // Вызов метода для загрузки названия товара
        }

        private void LoadProductTitle()
        {
            using (var context = new MyDBContext())
            {
                // Загружаем заказ с связанными данными
                var orderWithProducts = context.Orders
                    .Include(o => o.ProductHasOrders)
                    .ThenInclude(po => po.Product) // Загружаем связанные товары
                    .FirstOrDefault(o => o.OrderId == _order.OrderId);

                if (orderWithProducts != null)
                {
                    var productOrder = orderWithProducts.ProductHasOrders.FirstOrDefault();
                    if (productOrder != null && productOrder.Product != null)
                    {
                        ProductTitleTextBlock.Text = productOrder.Product.Title; // Устанавливаем название товара
                    }
                    else
                    {
                        ProductTitleTextBlock.Text = "В заказе нет товаров"; // Если в заказе нет товаров
                    }
                }
                else
                {
                    ProductTitleTextBlock.Text = "Заказ не найден"; // Если заказ не найден
                }
            }
        }

        private void LoadSuppliers()
        {
            using (var context = new MyDBContext())
            {
                var suppliers = context.Suppliers.ToList();
                SupplierComboBox.ItemsSource = suppliers;
                SupplierComboBox.DisplayMemberPath = "Title"; // Отображаем название поставщика
                SupplierComboBox.SelectedValuePath = "SupplierId"; // Устанавливаем значение для выбора
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyDBContext())
            {
                var orderToUpdate = context.Orders
                    .Include(o => o.ProductHasOrders) // Загружаем связанные данные
                    .FirstOrDefault(o => o.OrderId == _order.OrderId);
                if (orderToUpdate != null)
                {
                    // Обновляем количество товара
                    if (int.TryParse(QuantityTextBox.Text, out int quantity))
                    {
                        var productOrder = orderToUpdate.ProductHasOrders.FirstOrDefault();
                        if (productOrder != null)
                        {
                            productOrder.Quantity = quantity; // Обновляем количество
                        }
                    }

                    // Обновляем поставщика
                    if (SupplierComboBox.SelectedValue is int supplierId)
                    {
                        orderToUpdate.SupplierId = supplierId; // Обновляем поставщика
                    }

                    context.SaveChanges(); // Сохраняем изменения в базе данных

                    // Выводим сообщение об успешном редактировании
                    MessageBox.Show($"Заказ {orderToUpdate.OrderId} был успешно отредактирован.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            this.Close(); // Закрываем окно после сохранения
        }
    }
}
