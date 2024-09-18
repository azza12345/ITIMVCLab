using Microsoft.AspNetCore.Mvc;
using MVCLab.Models;

namespace MVCLab.Controllers
{
    public class CompanyController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult GetAll()
        {
            var companies = db.Companies.ToList();
            return View(companies);
        }

        public ActionResult GetById(int id)
        {
            var company = db.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }
    }
}
