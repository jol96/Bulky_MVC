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
        public ICategoryRepository Category { get; set; }
        public IProductRepository Product { get; set; }
        public IProductImageRepository ProductImage { get; set; }
        public ICompanyRepository Company { get; set; }
        public IShoppingCartRepository ShoppingCart { get; set; }
        public IApplicationUserRepository ApplicationUser { get; set; }
        public IOrderHeaderRepository OrderHeader { get; set; }
        public IOrderDetailRepository OrderDetail { get; set; }

        public UnitOfWork(ApplicationDbContext db, 
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IProductImageRepository productImageRepository,
            ICompanyRepository companyRepository,
            IShoppingCartRepository shoppingCartRepository,
            IApplicationUserRepository applicationUserRepository,
            IOrderHeaderRepository orderHeaderRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            _db = db;
            Category = categoryRepository;
            Product = productRepository;
            ProductImage = productImageRepository;
            Company = companyRepository;
            ShoppingCart = shoppingCartRepository;
            ApplicationUser = applicationUserRepository;
            OrderHeader = orderHeaderRepository;
            OrderDetail = orderDetailRepository;
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
                string innerExceptionMessage = ex.InnerException?.Message ?? "Inner Exception Message Unknown";
                string exceptionMessage = ex.Message ?? "Message Unknown";
                result = new OperationResult
                {

                    Error = new ErrorModel
                    {
                        ErrorCode = $"DbUpdateException. {innerExceptionMessage}",
                        ErrorMessage = exceptionMessage
                    },
                };   
            }
            catch (Exception ex)
            {
                string innerExceptionMessage = ex.InnerException?.Message ?? "Inner Exception Message Unknown";
                string exceptionMessage = ex.Message ?? "Message Unknown";
                result = new OperationResult
                {
                    Error = new ErrorModel
                    {
                        ErrorCode = $"DbUpdateException.  {innerExceptionMessage}",
                        ErrorMessage = exceptionMessage
                    },
                };
            }

            return result;
        }
    }
}
