using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Categories.Commands;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;
using smart_inventory.Models;

namespace smart_inventory.CQRS.Categories.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Check if category name already exists
            var existingCategory = await _unitOfWork.Categories
                .SingleOrDefaultAsync(c => c.Name == request.Name);
            
            if (existingCategory != null)
            {
                throw new InvalidOperationException($"Danh mục '{request.Name}' đã tồn tại");
            }

            var category = _mapper.Map<Category>(request);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
        }
    }
}