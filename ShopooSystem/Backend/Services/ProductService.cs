using Backend.Data;
using DataCommon.Entities;
using DataCommon.Request;
using DataCommon.Response;
using DataCommon.Response.ProductModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class ProductService
    {
        private readonly DBContext _context;
        public ProductService(DBContext context)
        {
            _context = context;
        }

        // Get all products
        public async Task<ResponseModel<GetProductListModel>> GetProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();

                foreach (var product in products)
                {
                    await _context.Entry(product).Reference(p => p.Category).LoadAsync();
                }

                return ResponseModel<GetProductListModel>.Success(new GetProductListModel(products));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get detail information of one product
        public async Task<ResponseModel<Product>> GetProduct(Guid id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);                

                if (product == null)
                {
                    return ResponseModel<Product>.NotFound();
                }

                await _context.Entry(product).Reference(p => p.Category).LoadAsync();

                return ResponseModel<Product>.Success(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get related products associated with one category
        public async Task<ResponseModel<GetProductListModel>> GetProductsByCategory(Guid categoryId)
        {
            try
            {
                var products = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();

                if (products == null || !products.Any())
                {
                    return ResponseModel<GetProductListModel>.NotFound();
                }

                return ResponseModel<GetProductListModel>.Success(new GetProductListModel(products));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Add new product
        public async Task<ResponseModel<Product>> PostProduct(ProductRequestModel productRequest)
        {
            try
            {
                var category = await _context.Categories.FindAsync(productRequest.CategoryId);
                if (category == null)
                {
                    return ResponseModel <Product>.BadRequest();
                }

                var product = new Product(productRequest, category);
                product.CreatedDate = DateTime.Now.ToString();

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return ResponseModel<Product>.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Edit information of a product
        public async Task<ResponseModel<Product>> PutProduct(Guid id, ProductRequestModel productRequest)
        {
            try
            {             
                if (id != productRequest.Id)
                {
                    return ResponseModel<Product>.BadRequest();
                }

                // Find Category of Product and Update Information
                var category = await _context.Categories.FindAsync(productRequest.CategoryId);
                if (category == null)
                {
                    return ResponseModel<Product>.BadRequest();
                }

                var product = new Product(productRequest, category);
                product.UpdatedDate = DateTime.Now.ToString();

                _context.Entry(product).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return ResponseModel<Product>.BadRequest();
                }

                return ResponseModel<Product>.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Delete one product
        public async Task<ResponseModel<Product>> DeleteProduct(Guid id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return ResponseModel<Product>.NotFound();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return ResponseModel<Product>.Success();
            }
            catch (Exception) { throw; }
        }
    }
}
