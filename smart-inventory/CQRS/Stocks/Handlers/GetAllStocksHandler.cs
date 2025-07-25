using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using smart_inventory.CQRS.Stocks.Queries;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;

namespace smart_inventory.CQRS.Stocks.Handlers
{
    public class GetAllStocksHandler : IRequestHandler<GetAllStocksQuery, IEnumerable<StockDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllStocksHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockDto>> Handle(GetAllStocksQuery request, CancellationToken cancellationToken)
        {
            var stocks = await _unitOfWork.Stocks.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<StockDto>>(stocks);
        }
    }
}