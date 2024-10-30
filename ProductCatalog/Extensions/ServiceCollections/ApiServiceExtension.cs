
using ProductCatalog.Application.Services.Product.Validations;
using ProductCatalog.Application.UseCases.Product;
using ProductCatalog.Application.UseCases.Product.Base;
using ProductCatalog.Application.UseCases.Supplier;
using ProductCatalog.Domain.Dtos.Product;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Domain.Ports.Product;
using ProductCatalog.Domain.Ports.Supplier;
using ProductCatalog.Domain.Ports.Validation;
using ProductCatalog.Infra.FluentValidation.Product;
using ProductCatalog.Infra.Mongo.Repositories;
using ProductCatalog.Interfaces;
using ProductCatalog.Responses;

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

            services.AddScoped<IPresenter, Presenter>();
            services.AddScoped<IOutputPort>(provider => provider.GetService<IPresenter>());

            services.AddScoped<IValidator<ICreateProductRequest>, CreateProductRequestValidator>();
            services.AddScoped<IValidator<IUpdateProductRequest>, UpdateProductRequestValidator>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
