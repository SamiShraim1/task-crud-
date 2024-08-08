using ConsoleApp4.context;
using ConsoleApp4.Models;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AppLicationDbContext())
            {
                // Ensure database is created
                context.Database.EnsureCreated();

                // Add data to Product table
                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        new Product { Name = "Laptop", Price = 999.99 },
                        new Product { Name = "Smartphone", Price = 499.99 },
                        new Product { Name = "Tablet", Price = 299.99 }
                    );
                    context.SaveChanges();
                }

                // Add data to Order table
                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(
                        new Order { Address = "123 Main St", CreatedAt = DateTime.Now },
                        new Order { Address = "456 Elm St", CreatedAt = DateTime.Now.AddMinutes(-30) },
                        new Order { Address = "789 Oak St", CreatedAt = DateTime.Now.AddHours(-1) }
                    );
                    context.SaveChanges();
                }

                // Get all products
                Console.WriteLine("Products:");
                foreach (var product in context.Products.ToList())
                {
                    Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}");
                }

                // Get all orders
                Console.WriteLine("\nOrders:");
                foreach (var order in context.Orders.ToList())
                {
                    Console.WriteLine($"Id: {order.Id}, Address: {order.Address}, CreatedAt: {order.CreatedAt}");
                }

                // Update product name
                var productToUpdate = context.Products.FirstOrDefault(p => p.Id == 1);
                if (productToUpdate != null)
                {
                    productToUpdate.Name = "Updated Laptop";
                    context.SaveChanges();
                }

                // Update order created at
                var orderToUpdate = context.Orders.FirstOrDefault(o => o.Id == 1);
                if (orderToUpdate != null)
                {
                    orderToUpdate.CreatedAt = DateTime.Now.AddDays(-1);
                    context.SaveChanges();
                }

                // Remove product with id 2
                var productToRemove = context.Products.FirstOrDefault(p => p.Id == 2);
                if (productToRemove != null)
                {
                    context.Products.Remove(productToRemove);
                    context.SaveChanges();
                }

                // Remove order with id 3
                var orderToRemove = context.Orders.FirstOrDefault(o => o.Id == 3);
                if (orderToRemove != null)
                {
                    context.Orders.Remove(orderToRemove);
                    context.SaveChanges();
                }

                // Display updated data
                Console.WriteLine("\nUpdated Products:");
                foreach (var product in context.Products.ToList())
                {
                    Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}");
                }

                Console.WriteLine("\nUpdated Orders:");
                foreach (var order in context.Orders.ToList())
                {
                    Console.WriteLine($"Id: {order.Id}, Address: {order.Address}, CreatedAt: {order.CreatedAt}");
                }
            }
        }
    }
}
