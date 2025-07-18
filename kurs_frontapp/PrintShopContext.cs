using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace kurs_frontapp
{
    public class PrintShopContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Order> order_ { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Order_Items> Order_Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=localhost;port=3307;database=print_shop;user=root;password=58QPGrcj;",
                    ServerVersion.AutoDetect("server=localhost;port=3307;database=print_shop;user=root;password=58QPGrcj;"),
                    options => options.EnableRetryOnFailure());
            }
        }
    }
}