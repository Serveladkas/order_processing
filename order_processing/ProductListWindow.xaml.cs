using Microsoft.EntityFrameworkCore;
using order_processing.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace order_processing
{
    public partial class ProductListWindow : Window
    {
        public ObservableCollection<Product> Products { get; set; }

        public ProductListWindow()
        {
            InitializeComponent();
            LoadProducts();
            DataContext = this; // Устанавливаем контекст данных
        }

        private void LoadProducts()
        {
            using (var context = new MyDBContext())
            {
                Products = new ObservableCollection<Product>(context.Products.ToList());
            }
        }
    }
}