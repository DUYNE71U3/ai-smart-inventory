using MediatR;
using Microsoft.AspNetCore.Mvc;
using smart_inventory.CQRS.Suppliers.Commands;
using smart_inventory.CQRS.Suppliers.Queries;
using smart_inventory.DTOs;
using AutoMapper;

namespace smart_inventory.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SupplierController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: Supplier
        public async Task<IActionResult> Index()
        {
            var suppliers = await _mediator.Send(new GetAllSuppliersQuery());
            return View(suppliers);
        }

        // GET: Supplier/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var supplier = await _mediator.Send(new GetSupplierByIdQuery { Id = id });
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // GET: Supplier/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSupplierDto createSupplierDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = _mapper.Map<CreateSupplierCommand>(createSupplierDto);
                    await _mediator.Send(command);
                    TempData["SuccessMessage"] = "Nhà cung cấp đã được tạo thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("Code", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo nhà cung cấp: " + ex.Message);
                }
            }
            return View(createSupplierDto);
        }

        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _mediator.Send(new GetSupplierByIdQuery { Id = id });
            if (supplier == null)
            {
                return NotFound();
            }

            var command = new UpdateSupplierCommand
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Code = supplier.Code,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Notes = supplier.Notes,
                IsActive = supplier.IsActive
            };

            return View(command);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateSupplierDto updateSupplierDto)
        {
            if (id != updateSupplierDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var command = _mapper.Map<UpdateSupplierCommand>(updateSupplierDto);
                    await _mediator.Send(command);
                    TempData["SuccessMessage"] = "Nhà cung cấp đã được cập nhật thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("Code", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật nhà cung cấp: " + ex.Message);
                }
            }
            return View(updateSupplierDto);
        }

        // GET: Supplier/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _mediator.Send(new GetSupplierByIdQuery { Id = id });
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _mediator.Send(new DeleteSupplierCommand { Id = id });
                TempData["SuccessMessage"] = "Nhà cung cấp đã được xóa thành công!";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa nhà cung cấp: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}