using DataCommon.Entities;
using DataCommon.Response;
using ShopooCustomerApp.Utils;
using System.Text.Json;

namespace ShopooCustomerApp.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var response = await _httpClient.GetAsync($"{ParamConfig.Instance.API_URL}/api/Product");
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<ResponseModel<List<Product>>>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return responseData?.Data;
            }

            return new List<Product>();
        }

        public async Task<List<Product>> GetProductsByCategory(Guid categoryId)
        {
            var response = await _httpClient.GetAsync($"{ParamConfig.Instance.API_URL}/api/Categories/{categoryId}/Products");
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<ResponseModel<List<Product>>>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return responseData?.Data;
            }

            return new List<Product>();
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            var response = await _httpClient.GetAsync($"{ParamConfig.Instance.API_URL}/api/Product/{productId}");
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<ResponseModel<Product>>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return responseData?.Data;
            }

            return new Product();
        }
    }
}
