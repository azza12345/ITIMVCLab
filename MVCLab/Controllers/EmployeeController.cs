using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCLab.Models;

namespace MVCLab.Controllers
{
    public class EmployeeController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult GetAll()
        {
            var employees = db.Employees.Include(e => e.Company).ToList();
            return View(employees);
        }

        public ActionResult GetById(int id)
        {
            var employee = db.Employees.Include(e => e.Company).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public ActionResult GetByCompanyName(string companyName)
        {
            var employees = db.Employees.Include(e => e.Company).Where(e => e.Company.Name == companyName).ToList();
            return View(employees);
        }
    }
}
