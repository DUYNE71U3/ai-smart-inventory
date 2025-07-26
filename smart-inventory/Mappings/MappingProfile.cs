using AutoMapper;
using smart_inventory.Models;
using smart_inventory.DTOs;
using smart_inventory.CQRS.Categories.Commands;
using smart_inventory.CQRS.Products.Commands;
using System.Linq;
using smart_inventory.Extensions;
using smart_inventory.CQRS.Suppliers.Commands;

namespace smart_inventory.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ProductCount, opt => opt.Ignore()); // Will be set manually

            CreateMap<CreateCategoryDto, Category>();
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<UpdateCategoryCommand, Category>();

            // Product mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name));

            CreateMap<CreateProductDto, Product>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<UpdateProductCommand, Product>();

            // Command mappings
            CreateMap<CreateCategoryDto, CreateCategoryCommand>();
            CreateMap<UpdateCategoryDto, UpdateCategoryCommand>();
            CreateMap<CreateProductDto, CreateProductCommand>();
            CreateMap<UpdateProductDto, UpdateProductCommand>();

            // Stock mappings
            CreateMap<Stock, StockDto>()
                .ForMember(dest => dest.TypeName, 
                    opt => opt.MapFrom(src => src.Type.GetDisplayName()))
                .ForMember(dest => dest.TotalAmount,
                    opt => opt.MapFrom(src => src.Details.Sum(d => d.Quantity * d.UnitPrice)));

            CreateMap<StockDetail, StockDetailDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductSKU,
                    opt => opt.MapFrom(src => src.Product.SKU))
                .ForMember(dest => dest.TotalPrice,
                    opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));

            // Supplier mappings
            CreateMap<Supplier, SupplierDto>()
                .ForMember(dest => dest.ProductCount,
                    opt => opt.MapFrom(src => src.Products.Count));
            CreateMap<CreateSupplierDto, CreateSupplierCommand>();
            CreateMap<UpdateSupplierDto, UpdateSupplierCommand>();
            CreateMap<CreateSupplierCommand, Supplier>();
            CreateMap<UpdateSupplierCommand, Supplier>();
        }
    }
}