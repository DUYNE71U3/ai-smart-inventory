using Microsoft.AspNetCore.Mvc;
using smart_inventory.Interfaces;
using smart_inventory.ViewModels;

namespace smart_inventory.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public DashboardController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var products = await _productService.GetAllProductsAsync();

            var viewModel = new DashboardViewModel
            {
                TotalCategories = categories.Count(),
                TotalProducts = products.Count(),
                ActiveProducts = products.Count(p => p.IsActive),
                InactiveProducts = products.Count(p => !p.IsActive),
                LowStockProducts = products.Where(p => p.Quantity <= 10 && p.Quantity > 0).ToList(),
                OutOfStockProducts = products.Where(p => p.Quantity == 0).ToList(),
                TotalInventoryValue = products.Where(p => p.IsActive).Sum(p => p.Price * p.Quantity),
                Categories = categories.ToList(),
                RecentProducts = products.OrderByDescending(p => p.CreatedAt).Take(5).ToList()
            };

            return View(viewModel);
        }
    }
}