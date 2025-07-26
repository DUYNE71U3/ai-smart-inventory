using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Suppliers.Commands;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;
using smart_inventory.Models;

namespace smart_inventory.CQRS.Suppliers.Handlers
{
    public class CreateSupplierHandler : IRequestHandler<CreateSupplierCommand, SupplierDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSupplierHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SupplierDto> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var isCodeUnique = await _unitOfWork.Suppliers.IsCodeUniqueAsync(request.Code);
            if (!isCodeUnique)
            {
                throw new InvalidOperationException($"Mã nhà cung cấp '{request.Code}' đã tồn tại");
            }

            var supplier = _mapper.Map<Supplier>(request);
            await _unitOfWork.Suppliers.AddAsync(supplier);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SupplierDto>(supplier);
        }
    }
}