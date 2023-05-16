using BulkyBook.Models;

namespace BulkyBookWeb.Services.Abstractions
{
    public interface ICompanyService
    {
        List<Company> GetCompanies();
        (bool isSuccess, string message) DeleteCompany(int? id);
    }
}
