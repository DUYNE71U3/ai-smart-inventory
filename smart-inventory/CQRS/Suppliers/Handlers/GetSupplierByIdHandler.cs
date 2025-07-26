using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Suppliers.Queries;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;

namespace smart_inventory.CQRS.Suppliers.Handlers
{
    public class GetSupplierByIdHandler : IRequestHandler<GetSupplierByIdQuery, SupplierDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSupplierByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SupplierDto?> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.Suppliers.GetSupplierWithProductsAsync(request.Id);
            return _mapper.Map<SupplierDto>(supplier);
        }
    }
}