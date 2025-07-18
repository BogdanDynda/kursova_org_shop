using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace kurs_frontapp
{
    public partial class OrdersPage : Page
    {
        private PrintShopContext _context = new PrintShopContext();

        public OrdersPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _context.order_
                .Include(o => o.Employee)
                .Include(o => o.Order_Items)
                .ThenInclude(i => i.Product)
                .Load();

            OrdersGrid.ItemsSource = _context.order_.Local.ToObservableCollection();
            EmployeeFilter.ItemsSource = _context.Employee.ToList();
        }

        private void DateFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void FilterChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }
        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            FromDatePicker.SelectedDate = null;
            ToDatePicker.SelectedDate = null;
            EmployeeFilter.SelectedIndex = -1;
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var query = _context.order_
                .Include(o => o.Employee)
                .Include(o => o.Order_Items)
                .AsQueryable();

            if (FromDatePicker.SelectedDate != null)
            {
                query = query.Where(o => o.order_datetime >= FromDatePicker.SelectedDate);
            }

            if (ToDatePicker.SelectedDate != null)
            {
                var toDate = ToDatePicker.SelectedDate.Value.AddDays(1);
                query = query.Where(o => o.order_datetime < toDate);
            }

            if (EmployeeFilter.SelectedValue != null)
            {
                int employeeId = (int)EmployeeFilter.SelectedValue;
                query = query.Where(o => o.employee_id == employeeId);
            }

            OrdersGrid.ItemsSource = query.ToList();
        }
    }
}