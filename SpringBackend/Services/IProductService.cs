using SpringBackend.Dtos;
using SpringBackend.Models;

namespace SpringBackend.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetProductsAsync(int? start = null, int? end = null);
        Task<ProductReadDto> GetProductAsync(string id);
        Task<ProductReadResponse> CreateProductAsync(ProductCreateDto productCreateDto);
    }
}