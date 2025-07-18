using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace kurs_frontapp
{
    public partial class ProductsPage : Page
    {
        private PrintShopContext _context = new PrintShopContext();

        public ProductsPage()
        {
            InitializeComponent();
            LoadData();
            ProductsGrid.SelectionChanged += ProductsGrid_SelectionChanged;
        }
        private void ProductsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void LoadData()
        {
            _context.Product
                .Include(p => p.Manufacturer)
                .Include(p => p.discount)
                .Load();

            ProductsGrid.ItemsSource = _context.Product.Local.ToObservableCollection();

            _context.Manufacturer.Load();
            ManufacturerFilter.ItemsSource = _context.Manufacturer.Local.ToObservableCollection();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }
        private void FilterChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }
        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
            ManufacturerFilter.SelectedIndex = -1;
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var query = _context.Product
                .Include(p => p.Manufacturer)
                .Include(p => p.discount)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                query = query.Where(p => p.name_.Contains(SearchBox.Text));
            }

            if (ManufacturerFilter.SelectedValue != null)
            {
                int manufacturerId = (int)ManufacturerFilter.SelectedValue;
                query = query.Where(p => p.manufacturer_id == manufacturerId);
            }

            ProductsGrid.ItemsSource = query.ToList();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ProductEditDialog();
            if (dialog.ShowDialog() == true)
            {
                _context.Product.Add(dialog.Product);
                _context.SaveChanges();
                ApplyFilters();
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selectedProduct)
            {
                var dialog = new ProductEditDialog(selectedProduct);
                if (dialog.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    ApplyFilters();
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть товар для редагування", "Попередження");
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selectedProduct)
            {
                if (MessageBox.Show("Ви впевнені, що хочете видалити цей товар?", "Підтвердження",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _context.Product.Remove(selectedProduct);
                    _context.SaveChanges();
                    ApplyFilters();
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть товар для видалення", "Попередження");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _context = new PrintShopContext();
            LoadData();
        }
    }
}