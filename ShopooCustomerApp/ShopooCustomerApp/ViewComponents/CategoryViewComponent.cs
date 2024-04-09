using Microsoft.AspNetCore.Mvc;
using ShopooCustomerApp.Services;

namespace ShopooCustomerApp.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _categoryService.GetAllCategories();
            return View(response.Categories);
        }
    }
}
