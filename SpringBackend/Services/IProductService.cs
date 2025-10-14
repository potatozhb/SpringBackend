using SpringBackend.Dtos;
using SpringBackend.Models;

namespace SpringBackend.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetProductsAsync(int? start, int? end);
        Task<ProductReadDto> GetProduct(string id);
        Task<ProductReadResponse> CreateProductAsync(ProductCreateDto productCreateDto);
    }
}