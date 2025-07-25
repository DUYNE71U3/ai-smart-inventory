using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using smart_inventory.CQRS.Stocks.Commands;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;
using smart_inventory.Models;
using smart_inventory.Models.ViewModels;

namespace smart_inventory.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IProductService _productService;

        public StockController(IStockService stockService, IProductService productService)
        {
            _stockService = stockService;
            _productService = productService;
        }

        // GET: Stock
        public async Task<IActionResult> Index()
        {
            var stocks = await _stockService.GetAllStocksAsync();
            return View(stocks);
        }

        // GET: Stock/Create
        public async Task<IActionResult> Create()
        {
            await LoadProductsSelectList();
            var model = new StockViewModel
            {
                DocumentDate = DateTime.Now,
                Details = new List<StockDetailViewModel>
                {
                    new StockDetailViewModel() // Tạo một dòng chi tiết mặc định
                }
            };
            return View(model);
        }

        // POST: Stock/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new CreateStockCommand
                    {
                        DocumentNo = model.DocumentNo,
                        Type = model.Type,
                        DocumentDate = model.DocumentDate,
                        Reference = model.Reference,
                        Notes = model.Notes,
                        Details = model.Details.Select(d => new CreateStockDetailCommand
                        {
                            ProductId = d.ProductId,
                            Quantity = d.Quantity,
                            UnitPrice = d.UnitPrice,
                            Notes = d.Notes
                        }).ToList()
                    };

                    var result = await _stockService.CreateStockAsync(command);
                    TempData["SuccessMessage"] = "Phiếu kho đã được tạo thành công!";
                    return RedirectToAction(nameof(Details), new { id = result.Id });
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo phiếu kho: " + ex.Message);
                }
            }

            await LoadProductsSelectList();
            return View(model);
        }

        // GET: Stock/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return View(stock);
        }

        private async Task LoadProductsSelectList(int? selectedId = null)
        {
            var products = await _productService.GetAllProductsAsync();
            ViewBag.Products = products.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"{p.Name} ({p.SKU}) - Tồn kho: {p.Quantity}",
                Selected = selectedId.HasValue && p.Id == selectedId.Value
            });
        }

        // AJAX: Get Product Details
        [HttpGet]
        public async Task<IActionResult> GetProductDetails(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            return Json(new
            {
                name = product.Name,
                sku = product.SKU,
                quantity = product.Quantity,
                price = product.Price
            });
        }
    }
}