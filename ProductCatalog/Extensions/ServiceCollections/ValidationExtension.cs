using FluentValidation;
using FluentValidation.AspNetCore;
using ProductCatalog.Application.Services.Product.Validations;

namespace ProductCatalog.Extensions.ServiceCollections
{
    public static class ValidationExtension
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();
            return services;
        }
    }
}
