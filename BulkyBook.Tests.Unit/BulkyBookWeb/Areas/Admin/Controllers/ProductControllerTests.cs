using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
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
            _sut = new ProductController(_unitOfWorkMock.Object, 
                _webHostEnvironmentMock.Object, 
                _productsServiceMock.Object, 
                _imageServiceMock.Object, 
                _loggerMock.Object);
        }


        [Fact]
        public void Index_ReturnsViewResult_WithListOfProducts()
        {
            // Arrange
            _unitOfWorkMock.Setup(_ => _.Product.GetAll(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .Returns((new List<Product> { new Product(), new Product() }, new OperationResult()));

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
                .Returns((new List<Product> { }, new OperationResult()));

            // Act
            var result = _sut.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Equal(0, model.Count());
        }
    }
}
