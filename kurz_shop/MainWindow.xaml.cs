using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace kurz_shop
{
    public partial class MainWindow : Window
    {
        private ShopContext _context = new ShopContext();
        private List<Product> _allProducts = new List<Product>();
        private List<Product> _cartProducts = new List<Product>();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження даних: {ex.Message}", "Помилка");
            }
        }

        private async Task LoadDataAsync()
        {
            _allProducts = await _context.Product
                .Include(p => p.Manufacturer)
                .Include(p => p.discount)
                .Where(p => p.stock_quantity > 0)
                .ToListAsync();

            ProductsItemsControl.ItemsSource = _allProducts;
            ManufacturerComboBox.ItemsSource = await _context.Manufacturer.ToListAsync();
            ManufacturerComboBox.SelectedIndex = -1;
        }

        // Обробник зміни тексту пошуку
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        // Обробник зміни фільтрів
        private void FilterChanged(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }
        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            ManufacturerComboBox.SelectedIndex = -1;
            PriceFromTextBox.Text = "";
            PriceToTextBox.Text = "";
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filtered = _allProducts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchTextBox.Text))
                filtered = filtered.Where(p => p.name_.Contains(SearchTextBox.Text));

            if (ManufacturerComboBox.SelectedValue != null)
                filtered = filtered.Where(p => p.Manufacturer_id == (int)ManufacturerComboBox.SelectedValue);

            if (decimal.TryParse(PriceFromTextBox.Text, out decimal minPrice))
                filtered = filtered.Where(p => p.price >= minPrice);

            if (decimal.TryParse(PriceToTextBox.Text, out decimal maxPrice))
                filtered = filtered.Where(p => p.price <= maxPrice);

            if (InStockCheckBox.IsChecked == true)
                filtered = filtered.Where(p => p.stock_quantity > 0);

            ProductsItemsControl.ItemsSource = filtered.ToList();
        }

        // Додавання товару до кошика
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).CommandParameter is Product product)
            {
                _cartProducts.Add(product);
                UpdateCart();
            }
        }

        // Видалення товару з кошика
        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).CommandParameter is Product product)
            {
                _cartProducts.Remove(product);
                UpdateCart();
            }
        }

        // Оновлення кошика
        private void UpdateCart()
        {
            CartListView.ItemsSource = null;
            CartListView.ItemsSource = _cartProducts;

            decimal total = _cartProducts.Sum(p => p.discount != null ?
                p.price * (100 - p.discount.percentage) / 100 :
                p.price);

            TotalTextBlock.Text = $"Всього: {total:C}";
        }

        // Оформлення замовлення
        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Замовлення оформлено!", "Інформація");
            _cartProducts.Clear();
            UpdateCart();
        }
    }
}