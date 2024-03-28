using DataCommon.Entities;
using DataCommon.Response;
using ShopooCustomerApp.Utils;
using System.Text.Json;

namespace ShopooCustomerApp.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categoriesResponse = await _httpClient.GetAsync($"{ParamConfig.Instance.API_URL}/api/Category");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                var categoriesJson = await categoriesResponse.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<ResponseModel<List<Category>>>(categoriesJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return categories?.Data;
            }

            return new List<Category>();
        }
    }
}
