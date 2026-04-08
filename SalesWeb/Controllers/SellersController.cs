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
        private readonly DepartmentService _departmentService;
        //private readonly SalesWebContext _context;

        public SellersController(SellersService sellersService, DepartmentService departmentService)
        {
            _sellersService = sellersService;
            _departmentService = departmentService;
            //_context = context;
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
            var departments = _departmentService.FindAll();
            var viewModel = new Models.ViewModels.SellerFormViewModel { Departments = departments };
            //ViewBag.Departments = new SelectList(_context.Department, "Id", "Name");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            //foreach (var kvp in ModelState)
            //{
            //    foreach (var error in kvp.Value.Errors)
            //    {
            //        Console.WriteLine($"Campo: {kvp.Key} | Erro: {error.ErrorMessage}");
            //    }
            //}

            //if (!ModelState.IsValid)
            //{
            //    ViewBag.Departments = new SelectList(_context.Department, "Id", "Name", obj.DepartmentId);
            //    return View(obj);
            //}

            _sellersService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            return View();
        }
    }
}