namespace ProductCatalog.Extensions.ServiceCollections
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceCollections(this IServiceCollection services, IConfiguration configuration) {

            services.AddApis();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMapper();
            services.AddMongoDB(configuration);
            services.AddCors(c => c.AddPolicy("AllowOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetPreflightMaxAge(TimeSpan.FromSeconds(1000));
            }));

            return services;

        }
    }
}
