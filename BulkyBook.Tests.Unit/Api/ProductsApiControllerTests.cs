using BulkyBook.Models;
using BulkyBook.Tests.Unit.Factory;
using BulkyBookWeb.Api;
using BulkyBookWeb.Api.models;
using BulkyBookWeb.Repository.Models;
using BulkyBookWeb.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BulkyBook.Tests.Unit.Api
{
    public class ProductsApiControllerTests
    {
        private readonly Mock<IProductsService> _productsServiceMock;
        private readonly ProductsApiController _sut;

        public ProductsApiControllerTests()
        {
            _productsServiceMock = new Mock<IProductsService>();
            _sut = new ProductsApiController(_productsServiceMock.Object);
        }

        [Fact]
        public void Upsert_NewValidProductRequest_ReturnsOk()
        {
            // Arrange
            var productRequest = ProductRequestFactory.CreateNewValidProductRequest();
            _productsServiceMock.Setup(_ => _.UpsertProduct(It.IsAny<Product>()))
                .Returns(new OperationResult { IsSuccess = true });

            // Act
            var result = _sut.Upsert(productRequest) as OkObjectResult;

            // Assert;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void Upsert_NewInvalidProductRequest_ReturnsBadRequest()
        {
            // Arrange
            var productRequest = ProductRequestFactory.CreateNewInvalidProductRequest();
            var response = new OperationResult 
                { 
                    IsSuccess = false,
                    Error = new ErrorModel
                    {
                        ErrorCode = "DbUpdate Exception",
                        ErrorMessage = "Invalid CategoryId"
                    }
                };
            _productsServiceMock.Setup(_ => _.UpsertProduct(It.IsAny<Product>()))
                .Returns(response);

            // Act
            var result = _sut.Upsert(productRequest) as BadRequestObjectResult;

            // Assert;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
    }
}
