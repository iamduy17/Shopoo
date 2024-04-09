using DataCommon.Entities;
using DataCommon.Response.CategoryModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using ShopooCustomerApp.Services;
using ShopooCustomerApp.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.CustomerApp
{
    [TestFixture]
    public class CategoryViewComponentTest
    {
        private CategoryViewComponent _categoryViewComponent;
        private Mock<ICategoryService> _mockCategoryService;

        [SetUp]
        public void Setup()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _categoryViewComponent = new CategoryViewComponent(_mockCategoryService.Object);
        }

        [Test]
        public async Task InvokeAsync_Success()
        {
            // Arrange
            var categoryList = new List<Category>()
            {
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Category1",
                    Description = ""
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Category2",
                    Description = ""
                }
            };

            _mockCategoryService.Setup(service => service.GetAllCategories())
                .ReturnsAsync(new GetCategoryListModel(categoryList));

            // Act
            var result = await _categoryViewComponent.InvokeAsync();

            // Assert
            Assert.IsInstanceOf<IViewComponentResult>(result);
            
            var viewResult = result as ViewViewComponentResult;
            Assert.IsNotNull(viewResult);
            Assert.IsNotNull(viewResult.ViewData.Model);
            Assert.IsInstanceOf<List<Category>>(viewResult.ViewData.Model);
            var model = (List<Category>)viewResult.ViewData.Model;
            Assert.AreEqual(categoryList, model);
        }
    }
}
