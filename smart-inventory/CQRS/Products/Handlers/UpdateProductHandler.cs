using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Products.Commands;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;

namespace smart_inventory.CQRS.Products.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy sản phẩm với ID: {request.Id}");
            }

            // Check if SKU already exists (excluding current product)
            var isSkuUnique = await _unitOfWork.Products.IsSkuUniqueAsync(request.SKU, request.Id);
            if (!isSkuUnique)
            {
                throw new InvalidOperationException($"Mã SKU '{request.SKU}' đã tồn tại");
            }

            // Check if category exists
            var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new InvalidOperationException($"Không tìm thấy danh mục với ID: {request.CategoryId}");
            }

            _mapper.Map(request, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();

            // Load updated product with category for return
            var updatedProduct = await _unitOfWork.Products.GetProductWithCategoryAsync(product.Id);
            return _mapper.Map<ProductDto>(updatedProduct);
        }
    }
}