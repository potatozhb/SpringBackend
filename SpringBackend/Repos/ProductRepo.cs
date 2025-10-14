
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SpringBackend.Data;
using SpringBackend.Models;

namespace SpringBackend.Repos
{
    public class ProductRepo : IProductRepo
    {
        private const int CacheTTLMin = 5;

        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ProductRepo> _logger;

        public ProductRepo(AppDbContext context, IMemoryCache cache, ILogger<ProductRepo> logger)
        {
            this._context = context;
            this._cache = cache;
            this._logger = logger;
        }


        public void CreateProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            this._context.products.Add(product);
            this._cache.Set(product.Id, product, TimeSpan.FromMinutes(CacheTTLMin));
            this._logger.LogInformation($"--> Add new data");
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await this._context.products.ToListAsync();
        }

        public async Task<Product?> GetProductAsync(Guid id)
        {
            if (this._cache.TryGetValue(id, out var data) && data is Product product)
            {
                this._logger.LogInformation($"--> Get a data from cache");
                return product;
            }

            product = await _context.products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null) this._cache.Set(id, product, TimeSpan.FromMinutes(CacheTTLMin));
            this._logger.LogInformation($"--> Get a data from db");
            return product;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await this._context.SaveChangesAsync() >= 0;
        }
    }
}