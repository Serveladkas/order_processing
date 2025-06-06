using Microsoft.EntityFrameworkCore;
using order_processing.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace order_processing
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Order> Orders { get; set; } = new();
        public ICommand ShowStockCommand { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadOrders();
            DataContext = this; // Устанавливаем контекст данных

            // Инициализация команды
            ShowStockCommand = new RelayCommand(ShowStock);
        }

        private void LoadOrders()
        {
            using (var context = new MyDBContext())
            {
                // Очищаем существующую коллекцию
                Orders.Clear();

                // Добавляем новые заказы из базы данных
                foreach (var order in context.Orders.ToList())
                {
                    Orders.Add(order);
                }
            }
        }


        private void ShowStock()
        {
            ProductListWindow productListWindow = new ProductListWindow();
            productListWindow.ShowDialog(); // Открываем окно со списком товаров
        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика для создания нового заказа
            OrderCreationPage orderCreationWindow = new OrderCreationPage();
            orderCreationWindow.ShowDialog();
            LoadOrders(); // Обновляем список заказов после создания
        }

        private void CheckТotificationOrder_Click(object sender, RoutedEventArgs e)
        {
            // Реализация проверки уведомлений о заказах
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var order = button.DataContext as Order;

            // Откройте новое окно редактирования заказа
            OrderEditPage orderEditWindow = new OrderEditPage(order);
            orderEditWindow.ShowDialog();
            LoadOrders(); // Обновляем список заказов после редактирования
        }

        private void CancelOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var order = button.DataContext as Order;

            var result = MessageBox.Show("Вы уверены, что хотите отменить заказ?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (var context = new MyDBContext())
                {
                    var orderToUpdate = context.Orders.Find(order.OrderId);
                    if (orderToUpdate != null)
                    {
                        orderToUpdate.Status = "отменён"; // Обновляем статус заказа
                        context.SaveChanges(); // Сохраняем изменения в базе данных
                    }
                }
                LoadOrders(); // Обновляем список заказов после отмены
            }
        }
    }

    // Реализация RelayCommand
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute();

        public void Execute(object? parameter) => _execute();
    }
}