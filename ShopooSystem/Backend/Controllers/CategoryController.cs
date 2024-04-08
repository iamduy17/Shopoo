using Backend.Data;
using Backend.Interfaces;
using Backend.Services;
using DataCommon.Entities;
using DataCommon.Response;
using DataCommon.Response.CategoryModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ResponseModel<GetCategoryListModel>> GetCategories()
        {
            try
            {
                return await _categoryService.GetCategories();
            }
            catch (Exception)
            {
                return ResponseModel<GetCategoryListModel>.Error();
            }
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<Category>> GetCategory(Guid id)
        {
            try
            {
                return await _categoryService.GetCategory(id);
            }
            catch (Exception)
            {
                return ResponseModel<Category>.Error();
            }
        }

        [HttpPost]
        public async Task<ResponseModel<Category>> PostCategory(Category category)
        {
            try
            {
                return await _categoryService.PostCategory(category);
            }
            catch (Exception)
            {
                return ResponseModel<Category>.Error();
            }
        }

        [HttpPut("{id}")]
        public async Task<ResponseModel<Category>> PutCategory(Guid id, Category category)
        {
            try
            {
                return await _categoryService.PutCategory(id, category);
            }
            catch (Exception)
            {
                return ResponseModel<Category>.Error();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel<Category>> DeleteCategory(Guid id)
        {
            try
            {
                return await _categoryService.DeleteCategory(id);
            }
            catch (Exception)
            {
                return ResponseModel<Category>.Error();
            }
        }
    }
}
