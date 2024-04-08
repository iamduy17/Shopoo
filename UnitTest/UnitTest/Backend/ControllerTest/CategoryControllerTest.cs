using Backend.Controllers;
using Backend.Interfaces;
using DataCommon.Entities;
using DataCommon.Response;
using DataCommon.Response.CategoryModel;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Backend.ControllerTest
{
    [TestFixture]
    public class CategoryControllerTest
    {
        private CategoryController _categoryController;
        private Mock<ICategoryService> _mockCategoryService;

        [SetUp] 
        public void Setup()
        {
            _mockCategoryService = new Mock<ICategoryService>();    
            _categoryController = new CategoryController(_mockCategoryService.Object);
        }

        #region GetCategories
        [Test]
        public async Task GetCategories_Success()
        {
            // Arrange
            var categoryList = new GetCategoryListModel()
            {
                Categories = new List<Category>()
                {
                    new Category()
                    {
                        Id = new Guid("4e27f3e0-25e4-40d7-ee2c-08dc4f0d0c5b"),
                        Name = "Category1",
                        Description = ""
                    }
                }
            };

            _mockCategoryService.Setup(service => service.GetCategories())
                .ReturnsAsync(ResponseModel<GetCategoryListModel>.Success(categoryList));

            // Act
            var result = await _categoryController.GetCategories();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
            Assert.AreEqual(categoryList, result?.Data);
        }

        [Test]
        public async Task GetCategories_Exception()
        {
            // Arrange
            _mockCategoryService.Setup(service => service.GetCategories())
                .ThrowsAsync(new Exception());

            // Act
            var result = await _categoryController.GetCategories();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("error", result?.ReturnCode);
        }
        #endregion

        #region GetCategory
        [Test]
        public async Task GetCategory_Success()
        {
            // Arrange
            var categoryId = new Guid("4e27f3e0-25e4-40d7-ee2c-08dc4f0d0c5b");
            var category = new Category()
            {
                Id = new Guid("4e27f3e0-25e4-40d7-ee2c-08dc4f0d0c5b"),
                Name = "Category1",
                Description = ""
            };

            _mockCategoryService.Setup(service => service.GetCategory(categoryId))
                .ReturnsAsync(ResponseModel<Category>.Success(category));

            // Act
            var result = await _categoryController.GetCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
            Assert.AreEqual(category, result?.Data);
        }

        [Test]
        public async Task GetCategory_Exception()
        {
            // Arrange
            var categoryId = new Guid("4e27f3e0-25e4-40d7-ee2c-08dc4f0d0c5b");

            _mockCategoryService.Setup(service => service.GetCategory(categoryId))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _categoryController.GetCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("error", result?.ReturnCode);
            Assert.AreEqual(null, result?.Data);
        }
        #endregion

        #region PostCategory
        [Test]
        public async Task PostCategory_Success()
        {
            // Arrange
            var categoryRequest = new Category()
            {
                Name = "Category2",
                Description = ""
            };

            _mockCategoryService.Setup(service => service.PostCategory(categoryRequest))
                .ReturnsAsync(ResponseModel<Category>.Success());

            // Act
            var result = await _categoryController.PostCategory(categoryRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
        }

        [Test]
        public async Task PostCategory_Exception()
        {
            // Arrange
            var categoryRequest = new Category()
            {
                Name = "Category2",
                Description = ""
            };

            _mockCategoryService.Setup(service => service.PostCategory(categoryRequest))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _categoryController.PostCategory(categoryRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("error", result?.ReturnCode);
        }
        #endregion

        #region PutCategory
        public async Task PutCategory_Success()
        {
            // Arrange
            var categoryId = new Guid("4e27f3e0-25e4-40d7-ee2c-08dc4f0d0c5b");
            var categoryRequest = new Category()
            {
                Id = new Guid("4e27f3e0-25e4-40d7-ee2c-08dc4f0d0c5b"),
                Name = "Category1",
                Description = ""
            };

            _mockCategoryService.Setup(service => service.PutCategory(categoryId, categoryRequest))
                .ReturnsAsync(ResponseModel<Category>.Success());

            // Act
            var result = await _categoryController.PutCategory(categoryId, categoryRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
        }

        public async Task PutCategory_Exception()
        {
            // Arrange
            var categoryId = new Guid("4e27f3e0-25e4-40d7-ee2c-08dc4f0d0c5b");
            var categoryRequest = new Category()
            {
                Id = new Guid("4e27f3e0-25e4-40d7-ee2c-08dc4f0d0c5b"),
                Name = "Category1",
                Description = ""
            };

            _mockCategoryService.Setup(service => service.PutCategory(categoryId, categoryRequest))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _categoryController.PutCategory(categoryId, categoryRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("error", result?.ReturnCode);
        }
        #endregion

        #region DeleteCategory
        [Test]
        public async Task DeleteCategory_Success()
        {
            // Arrange
            var categoryId = new Guid("4e27f3e0-25e4-40d7-ee2c-08dc4f0d0c5b");

            _mockCategoryService.Setup(service => service.DeleteCategory(categoryId))
                .ReturnsAsync(ResponseModel<Category>.Success());

            // Act
            var result = await _categoryController.DeleteCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
        }

        [Test]
        public async Task DeleteCategory_Exception()
        {
            // Arrange
            var categoryId = new Guid("4e27f3e0-25e4-40d7-ee2c-08dc4f0d0c5b");

            _mockCategoryService.Setup(service => service.DeleteCategory(categoryId))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _categoryController.DeleteCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("error", result?.ReturnCode);
        }
        #endregion
    }
}
