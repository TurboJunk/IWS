using IWS.Resource.Models;
using IWS.Web.Models;
using IWS.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace IWS.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductService product;

        public HomeController(ILogger<HomeController> logger, ProductService product)
        {
            _logger = logger;
            this.product = product;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productList = await product.GetProductsListAsync();
            return View(productList);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            if (ModelState.IsValid)
            {
                await product.CreateProductAsync(model);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pd = await product.GetProductAsync(id);
            if (pd != null)
            {
                return View(pd);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                await product.UpdateProductAsync(model);
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
