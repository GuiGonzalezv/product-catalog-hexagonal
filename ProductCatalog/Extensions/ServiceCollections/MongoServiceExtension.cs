using MongoDB.Driver;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Settings;

namespace ProductCatalog.Extensions.ServiceCollections
{
    public static class MongoServiceExtension
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            // Adicionar configuração do MongoDB
            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDB"));

            // Adicionar o cliente MongoDB
            services.AddSingleton<IMongoClient>(ServiceProvider =>
                new MongoClient(configuration.GetValue<string>("MongoDB:ConnectionString")));

            services.AddSingleton<IMongoDatabase>(ServiceProvider =>
                ServiceProvider.GetRequiredService<IMongoClient>().GetDatabase(configuration.GetValue<string>("MongoDB:DatabaseName")));


            return services;
        }
    }
}
