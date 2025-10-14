
using AutoMapper;
using SpringBackend.Dtos;
using SpringBackend.Models;
using SpringBackend.Repos;

namespace SpringBackend.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductService(IProductRepo repo, IMapper mapper, ILogger<ProductService> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _productRepo = repo;
        }

        public async Task<ProductReadResponse> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            _productRepo.CreateProduct(product);
            await _productRepo.SaveChangesAsync();
            return _mapper.Map<ProductReadResponse>(product);
        }

        public async Task<ProductReadDto> GetProductAsync(string id)
        {
            var bid = Guid.TryParse(id, out Guid guid);
            if (!bid)
            {
                _logger.LogError("--> Invalid parameter");
                throw new ArgumentException("Invalid parameter");
            }
            var product = await _productRepo.GetProductAsync(guid);
            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<IEnumerable<ProductReadDto>> GetProductsAsync(int? start = null, int? end = null)
        {
            _logger.LogInformation("--> Getting products....");
            IEnumerable<Product> products;

            if (start.HasValue && end.HasValue)
            {
                if (start < 0 || end <= start)
                {
                    _logger.LogError("--> Invalid paging parameters");
                    throw new ArgumentException("Invalid paging parameters");
                }

                _logger.LogInformation($"--> Getting products from index {start.Value} to {end.Value}....");
                products = await _productRepo.GetAllProductsAsync(start.Value, end.Value);
            }
            else
            {
                products = await _productRepo.GetAllProductsAsync();
            }

            if (!products.Any())
            {
                _logger.LogWarning($"--> No weather data");
                return Enumerable.Empty<ProductReadDto>();
            }

            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }
    }
}