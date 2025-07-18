using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace kurs_frontapp
{
    public partial class ProductEditDialog : Window
    {
        private PrintShopContext _context = new PrintShopContext();
        public Product Product { get; private set; }

        public ProductEditDialog()
        {
            InitializeComponent();
            Product = new Product();
            DataContext = Product;
            LoadComboBoxes();
        }

        public ProductEditDialog(Product productToEdit)
        {
            InitializeComponent();
            Product = productToEdit;
            DataContext = Product;
            LoadComboBoxes();

            NameBox.Text = Product.name_;
            DescriptionBox.Text = Product.description_;
            PriceBox.Text = Product.price.ToString();
            StockBox.Text = Product.stock_quantity.ToString();

            if (Product.discount_id.HasValue)
            {
                DiscountCombo.SelectedValue = Product.discount_id.Value;
            }
        }

        private void LoadComboBoxes()
        {
            _context.Discount.Load();
            DiscountCombo.ItemsSource = _context.Discount.Local.ToObservableCollection();
            _context.Manufacturer.Load();
            ManufacturerCombo.ItemsSource = _context.Manufacturer.Local.ToObservableCollection();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {

            Product.name_ = NameBox.Text;
            Product.description_ = DescriptionBox.Text;

            if (decimal.TryParse(PriceBox.Text, out decimal price))
            {
                Product.price = price;
            }

            if (int.TryParse(StockBox.Text, out int stock))
            {
                Product.stock_quantity = stock;
            }

            if (DiscountCombo.SelectedValue != null)
            {
                Product.discount_id = (int)DiscountCombo.SelectedValue;
            }
            else
            {
                Product.discount_id = null;
            }

            DialogResult = true;
            Close();
        }
    }
}