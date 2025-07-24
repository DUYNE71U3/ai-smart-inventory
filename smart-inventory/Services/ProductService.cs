using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Products.Commands;
using smart_inventory.CQRS.Products.Queries;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;

namespace smart_inventory.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await _mediator.Send(new GetAllProductsQuery());
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            return await _mediator.Send(new GetProductByIdQuery { Id = id });
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _mediator.Send(new GetProductsByCategoryQuery { CategoryId = categoryId });
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var command = _mapper.Map<CreateProductCommand>(createProductDto);
            return await _mediator.Send(command);
        }

        public async Task<ProductDto> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var command = _mapper.Map<UpdateProductCommand>(updateProductDto);
            return await _mediator.Send(command);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _mediator.Send(new DeleteProductCommand { Id = id });
        }
    }
}