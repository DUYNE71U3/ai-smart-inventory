using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Products.Commands;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;
using smart_inventory.Models;

namespace smart_inventory.CQRS.Products.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Check if SKU already exists
            var isSkuUnique = await _unitOfWork.Products.IsSkuUniqueAsync(request.SKU);
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

            var product = _mapper.Map<Product>(request);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            // Load product with category for return
            var createdProduct = await _unitOfWork.Products.GetProductWithCategoryAsync(product.Id);
            return _mapper.Map<ProductDto>(createdProduct);
        }
    }
}