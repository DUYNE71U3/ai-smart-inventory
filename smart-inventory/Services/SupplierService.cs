using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Suppliers.Commands;
using smart_inventory.CQRS.Suppliers.Queries;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;

namespace smart_inventory.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SupplierService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
        {
            return await _mediator.Send(new GetAllSuppliersQuery());
        }

        public async Task<SupplierDto?> GetSupplierByIdAsync(int id)
        {
            return await _mediator.Send(new GetSupplierByIdQuery { Id = id });
        }

        public async Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto createSupplierDto)
        {
            var command = _mapper.Map<CreateSupplierCommand>(createSupplierDto);
            return await _mediator.Send(command);
        }

        public async Task<SupplierDto> UpdateSupplierAsync(UpdateSupplierDto updateSupplierDto)
        {
            var command = _mapper.Map<UpdateSupplierCommand>(updateSupplierDto);
            return await _mediator.Send(command);
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            return await _mediator.Send(new DeleteSupplierCommand { Id = id });
        }

        public async Task<SupplierDto?> GetSupplierWithProductsAsync(int id)
        {
            return await _mediator.Send(new GetSupplierByIdQuery { Id = id });
        }
    }
}