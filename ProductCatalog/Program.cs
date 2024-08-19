using ProductCatalog.Extensions.ApplicationBuilders;
using ProductCatalog.Extensions.ServiceCollections;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();
        Configure(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Configura o resto da aplicação
        services.AddServiceCollections(configuration);
    }

    private static void Configure(WebApplication app)
    {
        app.MapControllers();
        app.AddApplicationBuilder();
    }
}
