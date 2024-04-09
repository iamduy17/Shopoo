using Backend.Data;
using Backend.Interfaces;
using Backend.Services;
using DataCommon.Entities;
using DataCommon.Request;
using DataCommon.Response.ProductModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Backend.ServiceTest
{
    [TestFixture]
    public class ProductServiceTest
    {
        private ServiceProvider _serviceProvider;
        private ProductService _productService;
        private DBContext _dbContext;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();

            // Configure in-memory database for testing
            serviceCollection.AddDbContext<DBContext>(options =>
            {
                options.UseInMemoryDatabase("TestDB");
            });

            // Add CategoryService to service collection
            serviceCollection.AddScoped<IProductService, ProductService>();

            // Build the service provider
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Retrieve instances from the service provider
            _dbContext = _serviceProvider.GetRequiredService<DBContext>();
            _productService = _serviceProvider.GetRequiredService<IProductService>() as ProductService;
        }

        #region GetProducts
        [Test]
        public async Task GetProducts_Success()
        {
            // Arrange
            var categoryId = new Guid("3fa85f64-5717-4572-b3fc-2c963f66afa6");
            var productList = new List<Product>()
            {
                new Product {
                    Id = Guid.NewGuid(),
                    Name= "product1",
                    Description= "",
                    Price= 11110,
                    ImageURL = "string",
                    CreatedDate = "3/27/2024 10:06:58 PM",
                    UpdatedDate = null,
                    RatingPoint = 0,
                    CategoryId = categoryId,
                    Category = new Category()
                    {
                        Name = "category1",
                        Description = "string",
                        Id= categoryId
                    }
                }
            };

            await _dbContext.Products.AddRangeAsync(productList);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _productService.GetProducts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
            // Assert.AreEqual(productList, result?.Data.Products);
        }

        [Test]
        public async Task GetProducts_Exception()
        {
            // Arrange
            var productService = new ProductService(null);

            // Act and Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await productService.GetProducts());
        }
        #endregion

        #region GetProduct
        [Test]
        public async Task GetProduct_Success()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Name = "product1",
                Description = "",
                Price = 11110,
                ImageURL = "string",
                CreatedDate = "3/27/2024 10:06:58 PM",
                UpdatedDate = null,
                RatingPoint = 0,
                CategoryId = categoryId,
                Category = new Category()
                {
                    Name = "category1",
                    Description = "string",
                    Id = categoryId
                }
            };

            _dbContext.Add(product);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _productService.GetProduct(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
            Assert.AreEqual(product, result?.Data);
        }
        #endregion

        #region GetProductsByCategory
        [Test]
        public async Task GetProductsByCategory_Success()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Category()
            {
                Name = "category1",
                Description = "string",
                Id = categoryId
            };
            var productList = new List<Product>()
            {
                new Product {
                    Id = Guid.NewGuid(),
                    Name= "product1",
                    Description= "",
                    Price= 11110,
                    ImageURL = "string",
                    CreatedDate = "3/27/2024 10:06:58 PM",
                    UpdatedDate = null,
                    RatingPoint = 0,
                    CategoryId = categoryId,
                    Category = category
                },
                new Product {
                    Id = Guid.NewGuid(),
                    Name= "product2",
                    Description= "",
                    Price= 11110,
                    ImageURL = "string",
                    CreatedDate = "3/27/2024 10:06:59 PM",
                    UpdatedDate = null,
                    RatingPoint = 0,
                    CategoryId = categoryId,
                    Category = category
                }
            };

            await _dbContext.Products.AddRangeAsync(productList);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _productService.GetProductsByCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
            Assert.AreEqual(productList.Count, result?.Data.Products.Count);
        }
        #endregion

        #region PostProduct
        [Test]
        public async Task PostProduct_Success()
        {
            // Arrange
            var product = new ProductRequestModel
            {
                Id = Guid.NewGuid(),
                Name = "product1",
                Description = "",
                Price = 11110,
                ImageURL = "string",
                CreatedDate = "3/27/2024 10:06:58 PM",
                UpdatedDate = null,
                CategoryId = new Guid("3fa85f64-5717-4572-b3fc-2c963f66afa6"),
            };

            // Act
            var result = await _productService.PostProduct(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
        }
        #endregion
        #region PutProduct
        [Test]
        public async Task PutProduct_Success()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Category()
            {
                Name = "category1",
                Description = "string",
                Id = categoryId
            };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var productId = Guid.NewGuid();          
            var productRequest = new ProductRequestModel
            {
                Id = productId,
                Name = "Modified Product",
                Description = "",
                Price = 11110,
                ImageURL = "string",
                CreatedDate = "3/27/2024 10:06:58 PM",
                UpdatedDate = null,
                CategoryId = categoryId,
            };

            var product = new Product(productRequest, category);
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _productService.PutProduct(productId, productRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);

            var updatedProduct = await _dbContext.Products.FindAsync(productId);
            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual("Modified Product", updatedProduct?.Name);
        }
        #endregion

        #region DeleteProduct
        [Test]
        public async Task DeleteProduct_Success()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Name = "product1",
                Description = "",
                Price = 11110,
                ImageURL = "string",
                CreatedDate = "3/27/2024 10:06:58 PM",
                UpdatedDate = null,
                RatingPoint = 0,
                CategoryId = categoryId,
                Category = new Category()
                {
                    Name = "category1",
                    Description = "string",
                    Id = categoryId
                }
            };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _productService.DeleteProduct(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);

            var deletedProduct = await _dbContext.Products.FindAsync(productId);
            Assert.IsNull(deletedProduct);
        }
        #endregion
    }
}
