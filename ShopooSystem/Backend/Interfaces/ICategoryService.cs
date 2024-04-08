using DataCommon.Response.CategoryModel;
using DataCommon.Response;
using DataCommon.Entities;

namespace Backend.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseModel<GetCategoryListModel>> GetCategories();
        Task<ResponseModel<Category>> GetCategory(Guid id);
        Task<ResponseModel<Category>> PostCategory(Category category);
        Task<ResponseModel<Category>> PutCategory(Guid id, Category category);
        Task<ResponseModel<Category>> DeleteCategory(Guid id);
    }
}
