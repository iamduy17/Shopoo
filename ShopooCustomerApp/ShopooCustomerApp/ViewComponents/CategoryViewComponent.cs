﻿using Microsoft.AspNetCore.Mvc;
using ShopooCustomerApp.Services;

namespace ShopooCustomerApp.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly CategoryService _categoryService;
        public CategoryViewComponent(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllCategories();
            return View(categories);
        }
    }
}