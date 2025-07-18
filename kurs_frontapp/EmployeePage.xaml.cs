using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace kurs_frontapp
{
    public partial class EmployeePage : Page
    {
        private PrintShopContext _context = new PrintShopContext();

        public EmployeePage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _context.Employee.Load();
            EmployeesGrid.ItemsSource = _context.Employee.Local.ToObservableCollection();
        }
    }
}