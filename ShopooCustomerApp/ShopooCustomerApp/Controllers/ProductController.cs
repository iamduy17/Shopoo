﻿using Microsoft.AspNetCore.Mvc;
using ShopooCustomerApp.Services;

namespace ShopooCustomerApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return PartialView("_ProductList", products);
        }

        public async Task<IActionResult> GetProductsByCategory(Guid categoryId)
        {
            var products = await _productService.GetProductsByCategory(categoryId);
            return PartialView("_ProductList", products);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View("ProductDetail", product);
        }
    }
}