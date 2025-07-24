using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Categories.Commands;
using smart_inventory.CQRS.Categories.Queries;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;

namespace smart_inventory.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            return await _mediator.Send(new GetAllCategoriesQuery());
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            return await _mediator.Send(new GetCategoryByIdQuery { Id = id });
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var command = _mapper.Map<CreateCategoryCommand>(createCategoryDto);
            return await _mediator.Send(command);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var command = _mapper.Map<UpdateCategoryCommand>(updateCategoryDto);
            return await _mediator.Send(command);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _mediator.Send(new DeleteCategoryCommand { Id = id });
        }
    }
}