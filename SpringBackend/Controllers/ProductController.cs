using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpringBackend.Dtos;
using SpringBackend.Models;
using SpringBackend.Services;

namespace SpringBackend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Get all datas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await this._productService.GetProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get data by page
        /// </summary>
        /// <returns></returns>
        [HttpGet("page")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByIndex(
            [FromQuery] int? start = null,
            [FromQuery] int? end = null)
        {
            var products = await this._productService.GetProductsAsync(start, end);
            return Ok(products);
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<ProductReadResponse>> CreateProductForUser(
                                       [FromBody] ProductCreateDto productCreateDto)
        {
            try
            {
                var productResponse = await _productService.CreateProductAsync(productCreateDto);
                return Created("", productResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating product");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

    }
}