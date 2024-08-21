using Refit;

namespace ProductCatalog.Infra.ExternalApis.Example
{
    public interface IMyApiExample
    {
        [Get("/data")]
        Task<HttpResponseMessage> GetDataAsync();
    }
}
