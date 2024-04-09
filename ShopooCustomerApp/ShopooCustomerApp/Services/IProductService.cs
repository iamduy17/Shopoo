using DataCommon.Entities;
using DataCommon.Response.ProductModel;

namespace ShopooCustomerApp.Services
{
    public interface IProductService
    {
        Task<GetProductListModel> GetAllProducts();
        Task<GetProductListModel> GetProductsByCategory(Guid categoryId);
        Task<Product> GetProductById(Guid productId);
    }
}
