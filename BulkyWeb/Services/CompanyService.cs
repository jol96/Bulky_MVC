using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.Services.Abstractions;

namespace BulkyBookWeb.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Company> GetCompanies()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return objCompanyList;
        }

        public (bool isSuccess, string message) DeleteCompany(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return (false, "Error while deleting");
            }

            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            return (true, "Company deleted successfully");
        }
    }
}
