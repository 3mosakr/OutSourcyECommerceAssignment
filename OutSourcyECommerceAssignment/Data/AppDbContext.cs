using Microsoft.EntityFrameworkCore;
using OutSourcyECommerceAssignment.Models;

namespace OutSourcyECommerceAssignment.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            List<Product> products = new()
            {
                new Product { Id = 1, Name = "Laptop", Description = "High performance laptop", Price = 999.99M, Stock = 10 },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest model smartphone", Price = 699.99M, Stock = 25 },
                new Product { Id = 3, Name = "Headphones", Description = "Noise-cancelling headphones", Price = 199.99M, Stock = 50 },
                new Product { Id = 4, Name = "Smartwatch", Description = "Feature-rich smartwatch", Price = 299.99M, Stock = 15 },
                new Product { Id = 5, Name = "Tablet", Description = "Lightweight tablet", Price = 399.99M, Stock = 20 }
            };
            List<Customer> customers = new()
            {
                new Customer { Id = 1, Name = "Alice Johnson", Email = "alice@aa.com", Phone = "123-456-7890" },
                new Customer { Id = 2, Name = "Bob Smith", Email = "bob@aa.com", Phone = "234-567-8901" },
                new Customer { Id = 3, Name = "Charlie Brown", Email = "charlie@aa.com", Phone = "345-678-9012" },
                new Customer { Id = 4, Name = "Diana Prince", Email = "diana@aa.com", Phone = "456-789-0123" },

            };
            List<Order> orders = new()
            {
                new Order { Id = 1, CustomerId = 1, OrderDate = new DateTime(2025, 5, 15), Status = "Pending", TotalPrice = 999.99M + 199.99M },// Laptop + Headphones
                new Order { Id = 2, CustomerId = 2, OrderDate = new DateTime(2025, 6, 10), Status = "Pending", TotalPrice = 699.99M },// Smartphone 
                new Order { Id = 3, CustomerId = 3, OrderDate = new DateTime(2025, 7, 20), Status = "Pending", TotalPrice = 299.99M + 399.99M}, // Smartwatch + Tablet
                new Order { Id = 4, CustomerId = 1, OrderDate = new DateTime(2025, 8, 8), Status = "Pending", TotalPrice = 699.99M + 199.99M + 299.99M}, // Smartphone + Headphones + Smartwatch
            };
            List<OrderProduc> orderProducts = new()
            {
                new OrderProduc { OrderId = 1, ProductId = 1, Quantity = 1, EachPrice = 999.99M }, // Laptop
                new OrderProduc { OrderId = 1, ProductId = 3, Quantity = 1, EachPrice = 199.99M }, // Headphones
                new OrderProduc { OrderId = 2, ProductId = 2, Quantity = 1, EachPrice = 699.99M }, // Smartphone
                new OrderProduc { OrderId = 3, ProductId = 4, Quantity = 1, EachPrice = 299.99M }, // Smartwatch
                new OrderProduc { OrderId = 3, ProductId = 5, Quantity = 1, EachPrice = 399.99M }, // Tablet
                new OrderProduc { OrderId = 4, ProductId = 2, Quantity = 1, EachPrice = 699.99M }, // Smartphone
                new OrderProduc { OrderId = 4, ProductId = 3, Quantity = 1, EachPrice = 199.99M }, // Headphones
                new OrderProduc { OrderId = 4, ProductId = 4, Quantity = 1, EachPrice = 299.99M }, // Smartwatch
            };

            // Configure relationships and constraints if necessary
            // Order entity configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.Status).IsRequired().HasMaxLength(10);
                entity.Property(o => o.TotalPrice).IsRequired().HasPrecision(18, 2);
                // one-to-many relationship with customer
                entity.HasOne(o => o.Customer)
                       .WithMany()
                       .HasForeignKey(o => o.CustomerId);
                // many-to-many relationship with Product
                entity.HasMany(o => o.Products)
                       .WithMany(p => p.Orders)
                       .UsingEntity<OrderProduc>(
                    r => r.HasOne(pt => pt.Product).WithMany(p => p.OrderProducts).HasForeignKey(pt => pt.ProductId),
                    l => l.HasOne(pt => pt.Order).WithMany(o => o.OrderProducts).HasForeignKey(pt => pt.OrderId),
                    j => j.HasKey(t => new { t.OrderId, t.ProductId })
                    );
                // Seed data
                entity.HasData(orders);
            });

            // Custoemer entity configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Phone).HasMaxLength(15);
                entity.HasIndex(c => c.Email).IsUnique();
                // Seed data
                entity.HasData(customers);
            });

            // Product entity configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description).HasMaxLength(500);
                entity.Property(p => p.Price).IsRequired().HasPrecision(18, 2);
                entity.Property(p => p.Stock).IsRequired();
                // Seed data
                entity.HasData(products);
            });
            // OrderProduc entity configuration
            modelBuilder.Entity<OrderProduc>(entity =>
            {
                entity.HasKey(op => new { op.OrderId, op.ProductId });
                entity.Property(op => op.Quantity).IsRequired();
                entity.Property(op => op.EachPrice).IsRequired().HasPrecision(18, 2);
                // Seed data
                entity.HasData(orderProducts);
            });

        }

    }
}
