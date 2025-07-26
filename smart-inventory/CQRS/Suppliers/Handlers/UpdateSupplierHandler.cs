using AutoMapper;
using MediatR;
using smart_inventory.CQRS.Suppliers.Commands;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;

namespace smart_inventory.CQRS.Suppliers.Handlers
{
    public class UpdateSupplierHandler : IRequestHandler<UpdateSupplierCommand, SupplierDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSupplierHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SupplierDto> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(request.Id);
            if (supplier == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy nhà cung cấp với ID: {request.Id}");
            }

            var isCodeUnique = await _unitOfWork.Suppliers.IsCodeUniqueAsync(request.Code, request.Id);
            if (!isCodeUnique)
            {
                throw new InvalidOperationException($"Mã nhà cung cấp '{request.Code}' đã tồn tại");
            }

            _mapper.Map(request, supplier);
            _unitOfWork.Suppliers.Update(supplier);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SupplierDto>(supplier);
        }
    }
}