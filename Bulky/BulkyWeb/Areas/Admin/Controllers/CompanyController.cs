using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Company)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Company());
            }
            else
            {
                Company company = _unitOfWork.Company.Get(x => x.Id == id);
                return View(company);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id != 0)
                {
                    _unitOfWork.Company.Update(company);
                    _unitOfWork.Save();
                    TempData["Success"] = "Company updated successfully";
                }
                else
                {
                    _unitOfWork.Company.Add(company);
                    _unitOfWork.Save();
                    TempData["Success"] = "Company created successfully";
                }
                return RedirectToAction("Index");
            }
            return View(company);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> companies = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = companies });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Company? company = _unitOfWork.Company.Get(x => x.Id == id);
            if (company == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(company);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Company deleted successfully" });
        }
        #endregion
    }
}