using AutoMapper;
using smart_inventory.Models;
using smart_inventory.DTOs;
using smart_inventory.CQRS.Categories.Commands;
using smart_inventory.CQRS.Products.Commands;

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
        }
    }
}