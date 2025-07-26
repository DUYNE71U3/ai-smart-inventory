using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Suppliers.Queries;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;

namespace smart_inventory.CQRS.Suppliers.Handlers
{
    public class GetAllSuppliersHandler : IRequestHandler<GetAllSuppliersQuery, IEnumerable<SupplierDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllSuppliersHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDto>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _unitOfWork.Suppliers.GetAllWithProductCountAsync();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }
    }
}