using MediatR;
using smart_inventory.CQRS.Categories.Commands;
using smart_inventory.Interfaces;

namespace smart_inventory.CQRS.Categories.Handlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
            if (category == null)
            {
                return false;
            }

            // Check if category has products
            var hasProducts = await _unitOfWork.Categories.HasProductsAsync(request.Id);
            if (hasProducts)
            {
                throw new InvalidOperationException("Không thể xóa danh mục đã có sản phẩm");
            }

            _unitOfWork.Categories.Delete(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}