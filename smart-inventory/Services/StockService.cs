using AutoMapper;
using MediatR;
using smart_inventory.DTOs;
using smart_inventory.Interfaces;
using smart_inventory.CQRS.Stocks.Commands;
using smart_inventory.CQRS.Stocks.Queries;

namespace smart_inventory.Services
{
    public class StockService : IStockService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public StockService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<StockDto> CreateStockAsync(CreateStockCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<StockDto> GetStockByIdAsync(int id)
        {
            return await _mediator.Send(new GetStockByIdQuery { Id = id });
        }

        public async Task<IEnumerable<StockDto>> GetAllStocksAsync()
        {
            return await _mediator.Send(new GetAllStocksQuery());
        }

        public async Task<bool> DeleteStockAsync(int id)
        {
            return await _mediator.Send(new DeleteStockCommand { Id = id });
        }
    }
}