using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Categories.Queries;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;

namespace smart_inventory.CQRS.Categories.Handlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Categories.GetCategoriesWithProductsAsync();
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            
            // Set product count for each category
            foreach (var dto in categoryDtos)
            {
                var category = categories.FirstOrDefault(c => c.Id == dto.Id);
                dto.ProductCount = category?.Products?.Count ?? 0;
            }
            
            return categoryDtos;
        }
    }
}