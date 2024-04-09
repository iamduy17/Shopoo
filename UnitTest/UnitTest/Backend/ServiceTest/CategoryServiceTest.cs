using Backend.Data;
using Backend.Interfaces;
using Backend.Services;
using DataCommon.Entities;
using DataCommon.Response.CategoryModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Backend.ServiceTest
{
    [TestFixture]
    public class CategoryServiceTest
    {
        private ServiceProvider _serviceProvider;
        private CategoryService _categoryService;
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
            serviceCollection.AddScoped<ICategoryService, CategoryService>();

            // Build the service provider
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Retrieve instances from the service provider
            _dbContext = _serviceProvider.GetRequiredService<DBContext>();
            _categoryService = _serviceProvider.GetRequiredService<ICategoryService>() as CategoryService;
        }

        #region GetCategories
        [Test]
        public async Task GetCategories_Success()
        {
            // Arrange
            var categoryList = new List<Category>()
            {
                new Category()
                {
                    Id = new Guid("4e27f3e0-25e4-40d7-ee2c-08dc4f0d0c5b"),
                    Name = "Category1",
                    Description = ""
                }
            };

            await _dbContext.Categories.AddRangeAsync(categoryList);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _categoryService.GetCategories();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
            Assert.AreEqual(categoryList, result?.Data.Categories);
        }

        [Test]
        public async Task GetCategories_Exception()
        {
            // Arrange
            var categoryService = new CategoryService(null);

            // Act and Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await categoryService.GetCategories());
        }
        #endregion

        #region GetCategory
        [Test]
        public async Task GetCategory_Success()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Category()
            {
                Id = categoryId,
                Name = "Category1",
                Description = ""
            };

            _dbContext.Add(category);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _categoryService.GetCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
            Assert.AreEqual(category, result?.Data);
        }
        #endregion

        #region PostCategory
        [Test]
        public async Task PostCategory_Success()
        {
            // Arrange
            var category = new Category()
            {
                Name = "Category1",
                Description = ""
            };

            // Act
            var result = await _categoryService.PostCategory(category);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);
        }
        #endregion

        #region PutCategory
        [Test]
        public async Task PutCategory_Success()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Category()
            {
                Id = categoryId,
                Name = "Category1",
                Description = ""
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            category.Name = "Modified Category";

            // Act
            var result = await _categoryService.PutCategory(categoryId, category);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);

            var updatedData = await _dbContext.Categories.FindAsync(categoryId);
            Assert.IsNotNull(updatedData);
            Assert.AreEqual("Modified Category", updatedData?.Name);
        }
        #endregion

        #region DeleteCategory
        [Test]
        public async Task DeleteCategory_Success()
        {
            // Arrage
            var categoryId = Guid.NewGuid();
            var category = new Category()
            {
                Id = categoryId,
                Name = "Category1",
                Description = ""
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _categoryService.DeleteCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result?.ReturnCode);

            var deletedData = await _dbContext.Categories.FindAsync(categoryId);
            Assert.IsNull(deletedData);
        }
        #endregion

    }
}
