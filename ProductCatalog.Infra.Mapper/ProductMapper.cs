using AutoMapper;
using ProductCatalog.Application.UseCases.Product.Base;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Infra.Mongo.DataModel;

namespace ProductCatalog.Infra.Mapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper() { 
            
            CreateMap<ProductModel, ProductDataModel>().ReverseMap();

            CreateMap<CreateProductRequest, ProductModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => true));


            CreateMap<UpdateProductRequest, ProductModel>();
            CreateMap<ProductModel, ProductResponse>();
        }
    }
}
