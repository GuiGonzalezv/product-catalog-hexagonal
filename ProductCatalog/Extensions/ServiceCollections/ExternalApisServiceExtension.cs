using Polly;
using Polly.Extensions.Http;
using ProductCatalog.Infra.ExternalApis.Example;
using Refit;

namespace ProductCatalog.Extensions.ServiceCollections
{
    public static class ExternalApisServiceExtension
    {
        public static IServiceCollection AddExternalApis(this IServiceCollection services)
        {

            var circuitBreakerPolicy = HttpPolicyExtensions
              .HandleTransientHttpError()
              .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));

            services.AddRefitClient<IMyApiExample>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.example.com"))
                .AddPolicyHandler(circuitBreakerPolicy);

            return services;
        }
    }
}
