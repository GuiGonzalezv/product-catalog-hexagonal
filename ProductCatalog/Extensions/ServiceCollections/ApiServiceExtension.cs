
using ProductCatalog.Application.Services.Product;
using ProductCatalog.Application.Services.Supplier;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Domain.Ports.Product;
using ProductCatalog.Domain.Ports.Supplier;
using ProductCatalog.Infra.Mongo.Repositories;

namespace ProductCatalog.Extensions.ServiceCollections
{
    public static class ApiServiceExtension
    {
        public static IServiceCollection AddApis(this IServiceCollection services)
        {
            
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();

            return services;
        }
    }
}
