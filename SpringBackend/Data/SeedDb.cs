using Microsoft.EntityFrameworkCore;
using SpringBackend.Models;

namespace SpringBackend.Data
{
    public static class SeedDb
    {
        public static void InitializeDb(IApplicationBuilder app, bool isDev)
        {
            using (var service = app.ApplicationServices.CreateScope())
            {
                Seed(service.ServiceProvider.GetService<AppDbContext>(), isDev);
            }
        }

        private static void Seed(AppDbContext context, bool isDev)
        {
            if (!isDev)
            {
                Console.WriteLine("--> Attempting to apply migration...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.products.Any())
            {
                Console.WriteLine("--> Seeding data...");
                var productList = new List<Product>();
                for (int i = 1; i <= 100; i++)
                {
                    productList.Add(new Product
                    {
                        Name = "product name" + i,
                        Description = "Description " + i,
                        Category = "Category " + i,
                        Brand = "Brand " + i,
                        Price = i,
                        StockQuantity = 100 * i,
                        SKU = "SKU " + i
                    });
                }
                context.products.AddRange(productList);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Data already exists.");
            }

        }
    }
}