using AutoMapper;
using ProductCatalog.Application.Services.Supplier.Base;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Infra.Mongo.DataModel;

namespace ProductCatalog.Infra.Mapper
{
    public class SupplierMapper : Profile
    {
        public SupplierMapper()
        {

            CreateMap<SupplierModel, SupplierDataModel>().ReverseMap();

            CreateMap<CreateSupplierRequest, SupplierModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => true));


            CreateMap<UpdateSupplierRequest, SupplierModel>();
            CreateMap<SupplierModel, SupplierResponse>();
        }
    }
}
