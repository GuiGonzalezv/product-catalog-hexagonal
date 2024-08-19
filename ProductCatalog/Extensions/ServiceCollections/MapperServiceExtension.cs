using ProductCatalog.Domain.Ports;
using ProductCatalog.Infra.Mapper;

namespace ProductCatalog.Extensions.ServiceCollections
{
    public static class MapperServiceExtension
    {
        public static IServiceCollection AddMapper(this IServiceCollection services) {

            services.AddScoped<IMapper, Mapper>();
            services.AddAutoMapper(typeof(Mapper));
            return services;
        }
    }
}
