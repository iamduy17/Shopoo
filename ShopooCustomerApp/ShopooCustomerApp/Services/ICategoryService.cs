using DataCommon.Response.CategoryModel;

namespace ShopooCustomerApp.Services
{
    public interface ICategoryService
    {
        Task<GetCategoryListModel> GetAllCategories();
    }
}
