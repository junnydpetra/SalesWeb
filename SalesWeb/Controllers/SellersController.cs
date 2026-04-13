using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesWeb.Data;
using SalesWeb.Models;
using SalesWeb.Models.ViewModels;
using SalesWeb.Services;
using SalesWeb.Services.Exceptions;
using System.Data;

namespace SalesWeb.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellersService _sellersService;
        private readonly DepartmentService _departmentService;

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

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellersService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
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

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

            return View(viewModel);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return NotFound();
            }
            try
            {
                _sellersService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DBConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}