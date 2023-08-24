using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BulkyBook.Tests.Unit.BulkyBook.DataAccess.Repository
{
    public class UnitOfWorkTests
    {
        private readonly Mock<ApplicationDbContext> _dbMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IProductImageRepository> _productImageRepositoryMock;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryMock;
        private readonly Mock<IApplicationUserRepository> _applicationUserRepositoryMock;
        private readonly Mock<IOrderHeaderRepository> _orderHeaderRepositoryMock;
        private readonly Mock<IOrderDetailRepository> _orderDetailRepositoryMock;
        private readonly UnitOfWork _sut;

        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbMock = new Mock<ApplicationDbContext>(options);
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _productImageRepositoryMock = new Mock<IProductImageRepository>();
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
            _applicationUserRepositoryMock = new Mock<IApplicationUserRepository>();
            _orderHeaderRepositoryMock = new Mock<IOrderHeaderRepository>();
            _orderDetailRepositoryMock = new Mock<IOrderDetailRepository>();
            //SeedData();
            _sut = new UnitOfWork(_dbMock.Object, 
                _categoryRepositoryMock.Object, 
                _productRepositoryMock.Object,
                _productImageRepositoryMock.Object,
                _companyRepositoryMock.Object,
                _shoppingCartRepositoryMock.Object,
                _applicationUserRepositoryMock.Object,
                _orderHeaderRepositoryMock.Object,
                _orderDetailRepositoryMock.Object);
        }

        [Fact]
        public void Save_WhenDbContextSaveChangesSucceeds_ReturnsSuccessResult()
        {

            // Arrange
            _dbMock.Setup(db => db.SaveChanges()).Verifiable();

            // Act
            var actualResult = _sut.Save();

            // Assert
            _dbMock.Verify(db => db.SaveChanges(), Times.Once);
            Assert.True(actualResult.IsSuccess);
            Assert.Null(actualResult.Error);
        }

        [Fact]
        public void Save_WhenDbContextSaveChangesThrowsDbUpdateException_ReturnsErrorResult()
        {
            // Arrange
            _dbMock.Setup(_ => _.SaveChanges()).Throws(new DbUpdateException());

            // Act
            var actualResult = _sut.Save();

            // Assert
            Assert.True(!actualResult.IsSuccess);
            Assert.Contains("DbUpdateException", actualResult.Error.ErrorCode);
        }













        public void SeedData()
        {
            // Seed categories
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
            };
            //_dbMock.Object.Categories.AddRange(categories);
            //_dbMock.Object.SaveChanges();
            foreach (var category in categories)
            {
                _categoryRepositoryMock.Setup(repo => repo.Add(category));
            }


            // Seed products
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Praesent vitae sodales libero. ",
                    ISBN = "SWD9999001",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1
                }
            };
            foreach (var product in products)
            {
                _productRepositoryMock.Setup(repo => repo.Add(product));
            }
        }
    }
}
