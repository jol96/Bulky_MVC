using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookWeb.Areas.Admin.Controllers;
using BulkyBookWeb.Repository.Models;
using BulkyBookWeb.Services.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace BulkyBook.Tests.Unit.BulkyBookWeb.Areas.Admin.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private readonly Mock<IProductsService> _productsServiceMock;
        private readonly Mock<IImageService> _imageServiceMock;
        private readonly Mock<ILogger<ProductController>> _loggerMock;
        private readonly ProductController _sut;

        public ProductControllerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _productsServiceMock = new Mock<IProductsService>();
            _imageServiceMock = new Mock<IImageService>();
            _loggerMock = new Mock<ILogger<ProductController>>();
            _sut = new ProductController(_unitOfWorkMock.Object, 
                _webHostEnvironmentMock.Object, 
                _productsServiceMock.Object, 
                _imageServiceMock.Object,
                _loggerMock.Object);
        }

        #region Index tests
        [Fact]
        public void Index_ReturnsViewResult_WithListOfProducts()
        {
            // Arrange
            _unitOfWorkMock.Setup(_ => _.Product.GetAll(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .Returns((new List<Product> { new Product(), new Product() }, new OperationResult() { IsSuccess = true}));

            // Act
            var result = _sut.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Index_ReturnsViewResult_WithEmptyListOfProducts()
        {
            // Arrange
            _unitOfWorkMock.Setup(_ => _.Product.GetAll(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .Returns((new List<Product> { }, new OperationResult() { IsSuccess = true }));

            // Act
            var result = _sut.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Equal(0, model.Count());
        }

        [Fact]
        public void Index_ReturnsErrorViewResult_WhenIsSuccessFalse()
        {
            // Arrange
            _unitOfWorkMock.Setup(_ => _.Product.GetAll(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .Returns((new List<Product> { }, new OperationResult() { IsSuccess = false }));

            // Act
            var result = _sut.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("ErrorView", viewResult.ViewName);
        }
        #endregion

        #region Upsert tests
        [Fact]
        public void Upsert_ReturnsErrorViewResult_WhenIsSuccessFalse()
        {
            // Arrange
            var operationResult = new OperationResult()
            {
                IsSuccess = false,
                Error = new ErrorModel
                {
                    ErrorCode = "An error occured",
                    ErrorMessage = "Could not find categorys" }
            };

            _unitOfWorkMock.Setup(_ => _.Category.GetAll(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .Returns((new List<Category> { }, operationResult));

            // Act
            var result = _sut.Upsert(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("ErrorView", viewResult.ViewName);
        }

        [Fact]
        public void Upsert_ReturnsUpsertViewResultToCreateProducts_WhenIsSuccessTrue()
        {
            // Arrange
            var operationResult = new OperationResult()
            {
                IsSuccess = true,
                Error = null
            };

            var categoryList = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    DisplayOrder = 1,
                    Name = "Comedy"
                },
                new Category
                {
                    Id = 2,
                    DisplayOrder = 2,
                    Name = "History"
                }
            };

            _unitOfWorkMock.Setup(_ => _.Category.GetAll(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .Returns((categoryList, operationResult));

            // Act
            var result = _sut.Upsert(null);

            // Assert view - the ViewName will be null because its the same view as the controller
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);

            // Assert model
            var productVM = Assert.IsType<ProductVM>(viewResult.Model);
            Assert.Equal(categoryList.Count, productVM.CategoryList.Count());
        }

        [Fact]
        public void Upsert_ReturnsUpsertViewResultToUpdateProducts_WhenIsSuccessTrue()
        {
            // Arrange
            var operationResult = new OperationResult()
            {
                IsSuccess = true,
                Error = null
            };

            var categoryList = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    DisplayOrder = 1,
                    Name = "Comedy"
                },
                new Category
                {
                    Id = 2,
                    DisplayOrder = 2,
                    Name = "History"
                }
            };

            var product = new Product
            {
                Id = 1,
                Title = "Test",
                Description = "Test",
                ISBN = "900-900-900",
                Author = "Test",
                ListPrice = 10.99,
                Price = 10.00,
                Price50 = 5.00,
                Price100 = 4.00,
                CategoryId = 1
            };

            _unitOfWorkMock.Setup(_ => _.Category.GetAll(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .Returns((categoryList, operationResult));

            _unitOfWorkMock.Setup(_ => _.Product.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>(), false))
                .Returns(product);

            // Act
            var result = _sut.Upsert(product.Id);

            // Assert view - the ViewName will be null because its the same view as the controller
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);

            // Assert model
            var productVM = Assert.IsType<ProductVM>(viewResult.Model);
            Assert.Equal(product.Id, productVM.Product.Id);
            Assert.Equal(product.Title, productVM.Product.Title);
        }
        #endregion
    }
}
