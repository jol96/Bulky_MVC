using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Tests.Unit.Factory;
using BulkyBookWeb.Repository.Models;
using BulkyBookWeb.Services;
using BulkyBookWeb.Services.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BulkyBook.Tests.Unit.Services
{
    public class ProductsServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IImageService> _imageServiceMock;
        private readonly ProductsService _sut;

        public ProductsServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork> ();
            _imageServiceMock = new Mock<IImageService>();
            _sut = new ProductsService(_unitOfWorkMock.Object, _imageServiceMock.Object);
        }

        [Fact]
        public void UpsertProduct_NewValidProduct_ReturnsSuccess()
        {
            // Arrange
            var product = ProductFactory.CreateNewValidProduct();
            _unitOfWorkMock.Setup(uow => uow.Product.Add(It.IsAny<Product>()));
            _unitOfWorkMock.Setup(uow => uow.Save()).Returns(new OperationResult { IsSuccess = true });
            
            // Act
            var actualResult = _sut.UpsertProduct(product);

            // Assert
            Assert.True(actualResult.IsSuccess);
        }

        [Fact]
        public void UpsertProduct_ExistingValidProduct_ReturnsSuccess()
        {
            // Arrange
            var product = ProductFactory.GetExistingValidProduct();
            _unitOfWorkMock.Setup(uow => uow.Product.Update(It.IsAny<Product>()));
            _unitOfWorkMock.Setup(uow => uow.Save()).Returns(new OperationResult { IsSuccess = true });

            // Act
            var actualResult = _sut.UpsertProduct(product);

            // Assert
            Assert.True(actualResult.IsSuccess);
        }

        [Fact]
        public void UpsertProduct_NewInvalidProduct_ReturnsError()
        {
            // Arrange
            var product = ProductFactory.CreateNewInvalidProduct();
            _unitOfWorkMock.Setup(uow => uow.Product.Add(It.IsAny<Product>()));
            _unitOfWorkMock.Setup(uow => uow.Save()).Returns(
                new OperationResult 
                { 
                    IsSuccess = false,
                    Error = new ErrorModel
                    {
                        ErrorCode = "DbUpdate Exception",
                        ErrorMessage = "Invalid CategoryId"
                    }
                });

            // Act
            var actualResult = _sut.UpsertProduct(product);

            // Assert
            Assert.True(!actualResult.IsSuccess);
        }
    }
}
