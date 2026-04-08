using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesWeb.Data;
using SalesWeb.Models;
using SalesWeb.Services;

namespace SalesWeb.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellersService _sellersService;
        private readonly SalesWebContext _context;

        public SellersController(SellersService sellersService, SalesWebContext context)
        {
            _sellersService = sellersService;
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _sellersService.FindAll();
            return View(list);
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Department, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller obj)
        {
            Console.WriteLine("=== ENTROU NO POST CREATE ===");

            Console.WriteLine($"Id: {obj.Id}");
            Console.WriteLine($"Name: {obj.Name}");
            Console.WriteLine($"Email: {obj.Email}");
            Console.WriteLine($"BirthDate: {obj.BirthDate}");
            Console.WriteLine($"BaseSalary: {obj.BaseSalary}");
            Console.WriteLine($"DepartmentId: {obj.DepartmentId}");

            Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");

            foreach (var kvp in ModelState)
            {
                foreach (var error in kvp.Value.Errors)
                {
                    Console.WriteLine($"Campo: {kvp.Key} | Erro: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(_context.Department, "Id", "Name", obj.DepartmentId);
                return View(obj);
            }

            _sellersService.Insert(obj);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            return View();
        }
    }
}