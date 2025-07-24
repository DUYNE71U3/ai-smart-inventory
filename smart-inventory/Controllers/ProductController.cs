using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;

namespace smart_inventory.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            await LoadCategoriesSelectList();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto createProductDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.CreateProductAsync(createProductDto);
                    TempData["SuccessMessage"] = "Sản phẩm đã được tạo thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    if (ex.Message.Contains("SKU"))
                    {
                        ModelState.AddModelError("SKU", ex.Message);
                    }
                    else if (ex.Message.Contains("danh mục"))
                    {
                        ModelState.AddModelError("CategoryId", ex.Message);
                    }
                    else
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo sản phẩm: " + ex.Message);
                }
            }

            await LoadCategoriesSelectList(createProductDto.CategoryId);
            return View(createProductDto);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,
                Quantity = product.Quantity,
                Price = product.Price,
                Location = product.Location,
                Notes = product.Notes,
                CategoryId = product.CategoryId,
                IsActive = product.IsActive
            };

            await LoadCategoriesSelectList(updateDto.CategoryId);
            return View(updateDto);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateProductDto updateProductDto)
        {
            if (id != updateProductDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateProductAsync(updateProductDto);
                    TempData["SuccessMessage"] = "Sản phẩm đã được cập nhật thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    if (ex.Message.Contains("SKU"))
                    {
                        ModelState.AddModelError("SKU", ex.Message);
                    }
                    else if (ex.Message.Contains("danh mục"))
                    {
                        ModelState.AddModelError("CategoryId", ex.Message);
                    }
                    else
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật sản phẩm: " + ex.Message);
                }
            }

            await LoadCategoriesSelectList(updateProductDto.CategoryId);
            return View(updateProductDto);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Sản phẩm đã được xóa thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không tìm thấy sản phẩm để xóa!";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa sản phẩm: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Product/ByCategory/5
        public async Task<IActionResult> ByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            
            ViewBag.CategoryName = category?.Name ?? "Không xác định";
            ViewBag.CategoryId = categoryId;
            
            return View("Index", products);
        }

        private async Task LoadCategoriesSelectList(int? selectedCategoryId = null)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedCategoryId);
        }
    }
}