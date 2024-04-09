using Backend.Controllers;
using Backend.Interfaces;
using Backend.Services;
using DataCommon.Entities;
using DataCommon.Request;
using DataCommon.Response;
using DataCommon.Response.ProductModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Backend.ControllerTest
{
    [TestFixture]
    public class ProductControllerTest
    {
        private ProductController _productController;
        private Mock<IProductService> __mockProductService;

        [SetUp]
        public void Setup()
        {
            __mockProductService = new Mock<IProductService>();
            _productController = new ProductController(__mockProductService.Object);
        }

        #region GetProducts
        [Test]  
        public async Task GetProducts_Success()
        {
            // Arrange
            // Initialize response data for MockService
            var productList = new GetProductListModel()
            {
                Products = new List<Product>()
                {
                    new Product {
                        Id = new Guid("3fa81f64-5717-4562-b3fc-2c963f66afa6"),
                        Name= "product1",
                        Description= "",
                        Price= 11110,
                        ImageURL = "string",
                        CreatedDate = "3/27/2024 10:06:58 PM",
                        UpdatedDate = null,
                        RatingPoint = 0,
                        CategoryId = new Guid("3fa85f64-5717-4572-b3fc-2c963f66afa6"),
                        Category = new Category()
                        {
                            Name = "category1",
                            Description = "string",
                            Id= new Guid("3fa85f64-5717-4572-b3fc-2c963f66afa6")
                        }
                    }
                }
            };

            // Setup service function calling and return success with data mock above
            __mockProductService.Setup(service => service.GetProducts())
                .ReturnsAsync(ResponseModel<GetProductListModel>.Success(productList));

            // Act
            // Setup Actual Response
            var result = await _productController.GetProducts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
            Assert.AreEqual(productList, result.Data);
        }

        [Test]
        public async Task GetProducts_Exception()
        {
            // Arrange
            // Setup MockService to throw exception
            __mockProductService.Setup(service => service.GetProducts())
                .ThrowsAsync(new Exception());

            // Act
            var result = await _productController.GetProducts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("error", result?.ReturnCode);
            Assert.IsNull(result?.Data);
        }
        #endregion

        #region GetProduct
        [Test]
        public async Task GetProduct_Success()
        {
            // Arrange mock
            var productId = new Guid("3fa81f64-5717-4562-b3fc-2c963f66afa6");
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
                CategoryId = new Guid("3fa85f64-5717-4572-b3fc-2c963f66afa6"),
                Category = new Category()
                {
                    Name = "category1",
                    Description = "string",
                    Id = new Guid("3fa85f64-5717-4572-b3fc-2c963f66afa6")
                }
            };
            __mockProductService.Setup(service => service.GetProduct(productId))
                .ReturnsAsync(ResponseModel<Product>.Success(product));


            // Act
            var result = await _productController.GetProduct(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
            Assert.AreEqual(product, result?.Data);
        }

        [Test]
        public async Task GetProduct_Exception()
        {
            // Arrange
            var productId = new Guid("3fa81f64-5717-4562-b3fc-2c963f66afa6");

            __mockProductService.Setup(service => service.GetProduct(productId))
                .ThrowsAsync(new Exception());
            
            // Act
            var result = await _productController.GetProduct(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("error", result?.ReturnCode);
            Assert.IsNull(result?.Data);
        }
        #endregion

        #region GetProductsByCategory
        [Test]
        public async Task GetProductsByCategory_Success()
        {
            // Arrange
            var categoryId = new Guid("3fa85f64-5717-4572-b3fc-2c963f66afa6");
            var productList = new GetProductListModel()
            {
                Products = new List<Product>()
                {
                    new Product {
                        Id = new Guid("3fa81f64-5717-4562-b3fc-2c963f66afa6"),
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
                            Id= new Guid("3fa85f64-5717-4572-b3fc-2c963f66afa6")
                        }
                    }
                }
            };

            __mockProductService.Setup(service => service.GetProductsByCategory(categoryId))
                .ReturnsAsync(ResponseModel<GetProductListModel>.Success(productList));

            // Act
            var result = await _productController.GetProductsByCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
            Assert.AreEqual(productList, result?.Data);
        }

        [Test]
        public async Task GetProductsByCategory_Exception()
        {
            // Arrange
            var categoryId = new Guid("3fa85f64-5717-4572-b3fc-2c963f66afa6");

            __mockProductService.Setup(service => service.GetProductsByCategory(categoryId))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _productController.GetProductsByCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("error", result?.ReturnCode);
            Assert.IsNull(result?.Data);
        }
        #endregion

        #region PostProduct
        [Test]
        public async Task PostProduct_Success()
        {
            // Arrange
            var productRequest = new ProductRequestModel()
            {
                Name = "product2",
                Description = "",
                Price = 11110,
                ImageURL = "string",
                CreatedDate = null,
                UpdatedDate = null,
                CategoryId = new Guid("4fa85f64-5717-4572-b3fc-2c963f66afa6"),
            };

            __mockProductService.Setup(service => service.PostProduct(productRequest))
                .ReturnsAsync(ResponseModel<Product>.Success());

            // Act
            var result = await _productController.PostProduct(productRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
        }

        [Test]
        public async Task PostProduct_Exception()
        {
            // Arrange
            var productRequest = new ProductRequestModel()
            {
                Name = "product2",
                Description = "",
                Price = 11110,
                ImageURL = "string",
                CreatedDate = null,
                UpdatedDate = null,
                CategoryId = new Guid("4fa85f64-5717-4572-b3fc-2c963f66afa6"),
            };

            __mockProductService.Setup(service => service.PostProduct(productRequest))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _productController.PostProduct(productRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("error", result?.ReturnCode);
        }
        #endregion

        #region PutProduct
        [Test]
        public async Task PutProduct_Success()
        {
            // Arrange
            var productId = new Guid("3fa81f64-5717-4562-b3fc-2c963f66afa6");
            var productRequest = new ProductRequestModel()
            {
                Id = new Guid("3fa81f64-5717-4562-b3fc-2c963f66afa6"),
                Name = "product2",
                Description = "",
                Price = 11110,
                ImageURL = "string",
                CreatedDate = "3/27/2024 10:06:58 PM",
                UpdatedDate = null,
                CategoryId = new Guid("4fa85f64-5717-4572-b3fc-2c963f66afa6"),
            };

            __mockProductService.Setup(service => service.PutProduct(productId, productRequest))
                .ReturnsAsync(ResponseModel<Product>.Success());

            // Act
            var result = await _productController.PutProduct(productId, productRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
        }

        [Test]
        public async Task PutProduct_Exception()
        {
            // Arrange
            var productId = new Guid("3fa81f64-5717-4562-b3fc-2c963f66afa6");
            var productRequest = new ProductRequestModel()
            {
                Id = new Guid("3fa81f64-5717-4562-b3fc-2c963f66afa6"),
                Name = "product2",
                Description = "",
                Price = 11110,
                ImageURL = "string",
                CreatedDate = "3/27/2024 10:06:58 PM",
                UpdatedDate = null,
                CategoryId = new Guid("4fa85f64-5717-4572-b3fc-2c963f66afa6"),
            };

            __mockProductService.Setup(service => service.PutProduct(productId, productRequest))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _productController.PutProduct(productId, productRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("error", result?.ReturnCode);
        }
        #endregion

        #region DeleteProduct
        [Test]
        public async Task DeleteProduct_Success()
        {
            // Arrange
            var productId = new Guid("3fa81f64-5717-4562-b3fc-2c963f66afa6");

            __mockProductService.Setup(service => service.DeleteProduct(productId))
                .ReturnsAsync(ResponseModel<Product>.Success());

            // Act
            var result = await _productController.DeleteProduct(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
        }

        [Test]
        public async Task DeleteProduct_Exception()
        {
            // Arrange
            var productId = new Guid("3fa81f64-5717-4562-b3fc-2c963f66afa6");

            __mockProductService.Setup(service => service.DeleteProduct(productId))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _productController.DeleteProduct(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("error", result?.ReturnCode);
        }
        #endregion
    }
}
