using Backend.Data;
using DataCommon.Entities;
using DataCommon.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class CategoryService
    {
        private readonly DBContext _context;
        public CategoryService(DBContext context)
        {
            _context = context;
        }

        // Get all categories
        public async Task<ResponseModel<List<Category>>> GetCategories()
        {
            try
            {
                var response = await _context.Categories.ToListAsync();
                return ResponseModel<List<Category>>.Success(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get detail information of one category
        public async Task<ResponseModel<Category>> GetCategory(Guid id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                {
                    return ResponseModel<Category>.NotFound();
                }

                return ResponseModel<Category>.Success(category);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Add new category
        public async Task<ResponseModel<Category>> PostCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return ResponseModel<Category>.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Edit information of category
        public async Task<ResponseModel<Category>> PutCategory(Guid id, Category category)
        {
            try
            {
                if (id != category.Id)
                {
                    return ResponseModel<Category>.BadRequest();
                }

                _context.Entry(category).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return ResponseModel<Category>.BadRequest();
                }

                return ResponseModel<Category>.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Delete category. This API will delete all related products associated with deleted category
        public async Task<ResponseModel<Category>> DeleteCategory(Guid id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return ResponseModel<Category>.NotFound();
                }

                _context.Categories.Remove(category); 
                await _context.SaveChangesAsync();

                return ResponseModel<Category>.Success();
            }
            catch (Exception) { throw; }
        }
    }
}
