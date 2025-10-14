using Microsoft.EntityFrameworkCore;
using SpringBackend.Data;
using SpringBackend.Models;

namespace TestProject
{
    public class UnitTest1
    {
        private async Task<AppDbContext> GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            // Seed sample data
            context.products.AddRange(
                new Product { Name = "Product1", Brand = "BrandA", Category = "Cat1", Price = 10, StockQuantity = 5, SKU = "SKU001" , Description="first product"},
                new Product { Name = "Product2", Brand = "BrandB", Category = "Cat2", Price = 20, StockQuantity = 10, SKU = "SKU002", Description = "second product" }
            );

            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task CanFetchAllProducts()
        {
            // Arrange
            var context = await GetInMemoryDbContext();

            // Act
            var products = await context.products.ToListAsync();

            // Assert
            Assert.Equal(2, products.Count);
        }

        [Fact]
        public async Task CanAddProduct()
        {
            // Arrange
            var context = await GetInMemoryDbContext();
            var newProduct = new Product
            {
                Name = "Product3",
                Description = "Product 3 description",
                Brand = "BrandC",
                Category = "Cat3",
                Price = 30,
                StockQuantity = 15,
                SKU = "SKU003"
            };

            // Act
            context.products.Add(newProduct);
            await context.SaveChangesAsync();

            var products = await context.products.ToListAsync();

            // Assert
            Assert.Equal(3, products.Count);
            Assert.Contains(products, p => p.Name == "Product3");
        }
    }
}