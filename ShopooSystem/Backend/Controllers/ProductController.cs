using Backend.Services;
using DataCommon.Entities;
using DataCommon.Request;
using DataCommon.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ResponseModel<List<Product>>> GetProducts()
        {
            try
            {
                return await _productService.GetProducts();
            }
            catch (Exception)
            {
                return ResponseModel<List<Product>>.Error();
            }
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<Product>> GetProduct(Guid id)
        {
            try
            {
                return await _productService.GetProduct(id);
            }
            catch (Exception)
            {
                return ResponseModel<Product>.Error();
            }
        }

        [HttpGet("~/api/Categories/{categoryId}/Products")]
        public async Task<ResponseModel<List<Product>>> GetProductsByCategory(Guid categoryId)
        {
            try
            {
                return await _productService.GetProductsByCategory(categoryId);
            }
            catch (Exception)
            {
                return ResponseModel<List<Product>>.Error();
            }
        }

        [HttpPost]
        public async Task<ResponseModel<Product>> PostProduct(ProductRequestModel product)
        {
            try
            {
                return await _productService.PostProduct(product);
            }
            catch (Exception)
            {
                return ResponseModel<Product>.Error();
            }
        }

        [HttpPut("{id}")]
        public async Task<ResponseModel<Product>> PutProduct(Guid id, ProductRequestModel product)
        {
            try
            {
                return await _productService.PutProduct(id, product);
            }
            catch (Exception)
            {
                return ResponseModel<Product>.Error();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel<Product>> DeleteProduct(Guid id)
        {
            try
            {
                return await _productService.DeleteProduct(id);
            }
            catch (Exception)
            {
                return ResponseModel<Product>.Error();
            }
        }
    }
}
