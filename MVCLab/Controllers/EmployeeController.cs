using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCLab.Models;

namespace MVCLab.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }


        public ActionResult GetAll()
        {
            var employees = _context.Employees.Include(e => e.Company).ToList();
            return View(employees);
        }


        public ActionResult GetById(int id)
        {
            var employee = _context.Employees.Include(e => e.Company).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }


        public IActionResult Create()
        {
            ViewBag.Companies = new SelectList(_context.Companies, "Id", "Name");
            return View();
        }


        [HttpPost]
        public IActionResult Create(Employee employee)
        {

            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("GetAll");
            }
            ViewBag.Companies = new SelectList(_context.Companies, "Id", "Name", employee.CompanyId);
            return View(employee);
        }


        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();

            var employee = _context.Employees.Find(id);
            if (employee == null) return NotFound();

            ViewBag.Companies = new SelectList(_context.Companies, "Id", "Name", employee.CompanyId); // Pass companies for the dropdown
            return View(employee);
        }


        [HttpPost]

        public IActionResult Update(int id, Employee employee)
        {
            if (id != employee.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(employee);
                _context.SaveChanges();
                return RedirectToAction("GetAll");
            }
            ViewBag.Companies = new SelectList(_context.Companies, "Id", "Name", employee.CompanyId); // Re-populate the dropdown
            return View(employee);
        }


        //        [HttpPost]
        //        public IActionResult Delete(int id)
        //        {
        //            try
        //            {
        //                var employee = _context.Employees.Find(id);
        //                if (employee == null)
        //                {
        //                    TempData["ErrorMessage"] = "Employee not found!";
        //                    return RedirectToAction("GetAll");
        //                }

        //                _context.Employees.Remove(employee);
        //                _context.SaveChanges();
        //                TempData["SuccessMessage"] = "Employee deleted successfully!";
        //            }
        //            catch (Exception ex)
        //            {
        //                TempData["ErrorMessage"] = "Error deleting employee: " + ex.Message;
        //            }


        //            return RedirectToAction("GetAll"); 
        //        }

        //        [HttpPost, ActionName("Delete")]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    var employee = _context.Employees.Find(id);
        //    if (employee != null)
        //    {
        //        _context.Employees.Remove(employee);
        //        _context.SaveChanges();
        //    }

        //    return RedirectToAction("GetAll");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Employee deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Employee not found.";
            }

            return RedirectToAction(nameof(GetAll));
        }

    }
}