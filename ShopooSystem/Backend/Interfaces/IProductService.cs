using DataCommon.Entities;
using DataCommon.Request;
using DataCommon.Response;
using DataCommon.Response.ProductModel;

namespace Backend.Interfaces
{
    public interface IProductService
    {
        Task<ResponseModel<GetProductListModel>> GetProducts();
        Task<ResponseModel<Product>> GetProduct(Guid id);
        Task<ResponseModel<GetProductListModel>> GetProductsByCategory(Guid categoryId);
        Task<ResponseModel<Product>> PostProduct(ProductRequestModel productRequest);
        Task<ResponseModel<Product>> PutProduct(Guid id, ProductRequestModel productRequest);
        Task<ResponseModel<Product>> DeleteProduct(Guid id);

    }
}
