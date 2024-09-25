using Microsoft.AspNetCore.Mvc;
using MVCLab.Models;

namespace MVCLab.Controllers
{
    public class CompanyController : Controller
    {
        
        private readonly AppDbContext _context;

        public CompanyController(AppDbContext context)
        {
            _context = context;
        }

        public ActionResult GetAll()
        {
            var companies = _context.Companies.ToList();
            return View(companies);
        }

        public ActionResult GetById(int id)
        {
            var company = _context.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

       
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
       
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Companies.Add(company);
                _context.SaveChanges();
                return RedirectToAction("GetAll");
            }
            return View(company);
        }

        
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();

            var company = _context.Companies.Find(id);
            if (company == null) return NotFound();

            return View(company);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Company company)
        {
            if (id != company.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(company);
                _context.SaveChanges();
                return RedirectToAction("GetAll");
            }
            return View(company);
        }
    }
}
