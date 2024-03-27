using Backend.Data;
using Backend.Services;
using DataCommon.Entities;
using DataCommon.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ResponseModel<List<Category>>> GetCategories()
        {
            try
            {
                return await _categoryService.GetCategories();
            }
            catch (Exception)
            {
                return ResponseModel<List<Category>>.Error();
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
