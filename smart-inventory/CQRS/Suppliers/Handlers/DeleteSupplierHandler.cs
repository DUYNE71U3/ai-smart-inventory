using MediatR;
using smart_inventory.CQRS.Suppliers.Commands;
using smart_inventory.Interfaces;

namespace smart_inventory.CQRS.Suppliers.Handlers
{
    public class DeleteSupplierHandler : IRequestHandler<DeleteSupplierCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSupplierHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.Suppliers.GetSupplierWithProductsAsync(request.Id);
            if (supplier == null)
                return false;

            if (supplier.Products.Any())
            {
                throw new InvalidOperationException(
                    $"Không thể xóa nhà cung cấp '{supplier.Name}' vì đang có {supplier.Products.Count} sản phẩm liên kết");
            }

            _unitOfWork.Suppliers.Delete(supplier);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}