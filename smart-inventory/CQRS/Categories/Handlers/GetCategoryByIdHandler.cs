using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Categories.Queries;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;

namespace smart_inventory.CQRS.Categories.Handlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetCategoryWithProductsAsync(request.Id);
            if (category == null)
                return null;

            var categoryDto = _mapper.Map<CategoryDto>(category);
            categoryDto.ProductCount = category.Products?.Count ?? 0;
            
            return categoryDto;
        }
    }
}