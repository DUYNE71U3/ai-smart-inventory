using MediatR;
using AutoMapper;
using smart_inventory.CQRS.Stocks.Commands;
using smart_inventory.DTOs;
using smart_inventory.Models;
using smart_inventory.Interfaces;

namespace smart_inventory.CQRS.Stocks.Handlers
{
    public class CreateStockHandler : IRequestHandler<CreateStockCommand, StockDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateStockHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StockDto> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra số chứng từ đã tồn tại chưa
            var existingStock = await _unitOfWork.Stocks
                .SingleOrDefaultAsync(s => s.DocumentNo == request.DocumentNo);
            
            if (existingStock != null)
            {
                throw new InvalidOperationException($"Số chứng từ '{request.DocumentNo}' đã tồn tại");
            }

            // Tạo phiếu nhập/xuất kho mới
            var stock = new Stock
            {
                DocumentNo = request.DocumentNo,
                Type = request.Type,
                DocumentDate = request.DocumentDate,
                Reference = request.Reference,
                Notes = request.Notes,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // Thêm chi tiết phiếu
            foreach (var detail in request.Details)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Không tìm thấy sản phẩm với ID: {detail.ProductId}");
                }

                // Kiểm tra số lượng khi xuất kho
                if (request.Type == StockType.StockOut && product.Quantity < detail.Quantity)
                {
                    throw new InvalidOperationException(
                        $"Sản phẩm '{product.Name}' không đủ số lượng xuất (Còn: {product.Quantity}, Yêu cầu: {detail.Quantity})");
                }

                stock.Details.Add(new StockDetail
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice,
                    Notes = detail.Notes
                });

                // Cập nhật số lượng sản phẩm
                if (request.Type == StockType.StockIn)
                {
                    product.Quantity += detail.Quantity;
                }
                else
                {
                    product.Quantity -= detail.Quantity;
                }
                
                _unitOfWork.Products.Update(product);
            }

            await _unitOfWork.Stocks.AddAsync(stock);
            await _unitOfWork.SaveChangesAsync();

            // Map và trả về DTO
            var stockDto = _mapper.Map<StockDto>(stock);
            return stockDto;
        }
    }
}