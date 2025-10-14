
using SpringBackend.Models;

namespace SpringBackend.Repos
{
    public interface IProductRepo
    {
        void CreateProduct(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductAsync(Guid id);

        Task<bool> SaveChangesAsync();
    }
}