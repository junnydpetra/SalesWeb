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
        }

        public IActionResult Index()
        {
            var list = _sellersService.FindAll();
            return View(list);
        }

        public IActionResult Details(int id)
        {
            var seller = _sellersService.FindById(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
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
            _sellersService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        public IActionResult Delete(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = _sellersService.FindById(id.Value);
            if (seller == null)
            {
                return NotFound();              
            }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        { 
            _sellersService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}