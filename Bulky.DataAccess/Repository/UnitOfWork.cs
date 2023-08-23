using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using BulkyBookWeb.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IProductImageRepository ProductImage { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            ProductImage = new ProductImageRepository(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
        }

        public OperationResult Save()
        {
            OperationResult result;

            try
            {
                _db.SaveChanges();
                result = new OperationResult
                {
                    IsSuccess = true
                };
            }
            catch (DbUpdateException ex)
            {
                result = new OperationResult
                {
                    Error = new ErrorModel
                    {
                        ErrorCode = $"DbUpdateException. InnerException message: {ex.InnerException.Message}",
                        ErrorMessage = ex.Message
                    },
                };
            }
            catch (Exception ex)
            {
                result = new OperationResult
                {
                    Error = new ErrorModel
                    {
                        ErrorCode = $"Generic exception. InnerException message: {ex.InnerException.Message}",
                        ErrorMessage = ex.Message
                    },
                };
            }

            return result;
        }
    }
}
