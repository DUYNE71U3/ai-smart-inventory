using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Categories.Commands;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;

namespace smart_inventory.CQRS.Categories.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy danh mục với ID: {request.Id}");
            }

            // Check if name already exists (excluding current category)
            var existingCategory = await _unitOfWork.Categories
                .SingleOrDefaultAsync(c => c.Name == request.Name && c.Id != request.Id);
            
            if (existingCategory != null)
            {
                throw new InvalidOperationException($"Danh mục '{request.Name}' đã tồn tại");
            }

            _mapper.Map(request, category);
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
        }
    }
}