using DataCommon.Entities;
using DataCommon.Response;
using DataCommon.Response.ProductModel;
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

        public async Task<GetProductListModel> GetAllProducts()
        {
            var response = await _httpClient.GetAsync($"{ParamConfig.Instance.API_URL}/api/Product");
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<ResponseModel<GetProductListModel>>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return responseData?.Data;
            }

            return new GetProductListModel();
        }

        public async Task<GetProductListModel> GetProductsByCategory(Guid categoryId)
        {
            var response = await _httpClient.GetAsync($"{ParamConfig.Instance.API_URL}/api/Categories/{categoryId}/Products");
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<ResponseModel<GetProductListModel>>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return responseData?.Data;
            }

            return new GetProductListModel();
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
