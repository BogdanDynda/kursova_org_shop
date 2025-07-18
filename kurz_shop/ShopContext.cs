using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurz_shop
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Discount> Discount { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Налаштування відображення назв таблиць і стовпців
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");
                entity.Property(e => e.product_id).HasColumnName("product_id");
                entity.Property(e => e.name_).HasColumnName("name_");
                entity.Property(e => e.description_).HasColumnName("description_");
                entity.Property(e => e.stock_quantity).HasColumnName("stock_quantity");
                entity.Property(e => e.price).HasColumnName("Price");
                entity.Property(e => e.Manufacturer_id).HasColumnName("Manufacturer_id");
                entity.Property(e => e.discount_id).HasColumnName("discount_id");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("manufacturer");
                entity.Property(e => e.Manufacturer_id).HasColumnName("Manufacturer_id");
                entity.Property(e => e.name_).HasColumnName("name_");
                entity.Property(e => e.country).HasColumnName("country");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("discount");
                entity.Property(e => e.discount_id).HasColumnName("discount_id");
                entity.Property(e => e.discount_code).HasColumnName("discount_code");
                entity.Property(e => e.percentage).HasColumnName("percentage");
            });
        }
    }
}