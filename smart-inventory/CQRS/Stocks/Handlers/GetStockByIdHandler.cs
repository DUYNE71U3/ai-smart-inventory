using MediatR;
using AutoMapper;
using smart_inventory.CQRS.Stocks.Queries;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace smart_inventory.CQRS.Stocks.Handlers
{
    public class GetStockByIdHandler : IRequestHandler<GetStockByIdQuery, StockDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStockByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StockDto> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
        {
            var stock = await _unitOfWork.Stocks
                .GetStockWithDetailsAsync(request.Id);

            if (stock == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy phiếu kho với ID: {request.Id}");
            }

            return _mapper.Map<StockDto>(stock);
        }
    }
}