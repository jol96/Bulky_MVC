using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Api
{
    public class CompanyApiController: Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyApiController(ICompanyService companyService, IUnitOfWork unitOfWork)
        {
            _companyService = companyService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all companies
        /// </summary>
        /// <returns>A list of companies</returns>
        [HttpGet]
        [Route("api/companyapi/getall")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var companyList = _companyService.GetCompanies();
            return Ok(Json(new { data = companyList }));
        }

        /// <summary>
        /// Deletes a company
        /// </summary>
        [HttpDelete]
        [Route("api/companyapi/delete")]
        [Produces("application/json")]
        public IActionResult Delete(int? id)
        {
            var (isSuccess, message) = _companyService.DeleteCompany(id);

            if(!isSuccess)
            {
                return BadRequest(message);
            }
            else
            {
                return Ok(Json(new { data = message }));
            }  
        }
    }
}
