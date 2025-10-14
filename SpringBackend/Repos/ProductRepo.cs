
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SpringBackend.Data;
using SpringBackend.Dtos;
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

        public async Task<IEnumerable<Product>> GetAllProductsAsync(int start, int end)
        {
            return await this._context.products
            .Skip(start)
            .Take(end - start)
            .ToListAsync();
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

        public async Task<IEnumerable<Product>> SearchProductsAsync(ProductSearchDto filter)
        {
            var query = _context.products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.Category))
                query = query.Where(p => p.Category == filter.Category);

            if (!string.IsNullOrWhiteSpace(filter.Brand))
                query = query.Where(p => p.Brand == filter.Brand);

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            if (filter.MinStock.HasValue)
                query = query.Where(p => p.StockQuantity >= filter.MinStock.Value);

            if (filter.MaxStock.HasValue)
                query = query.Where(p => p.StockQuantity <= filter.MaxStock.Value);

            query = query.OrderBy(p => p.Name);

            var products = await query.ToListAsync();

            return products;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await this._context.SaveChangesAsync() >= 0;
        }

    }
}