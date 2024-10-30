using ProductCatalog.Domain.Entities;
using System.Text.Json.Serialization;

namespace ProductCatalog.Domain.Ports
{
    public class UseCaseOutput<T> where T : class
    {
        public UseCaseOutput(T data)
        {
            Data = data;
        }

        public UseCaseOutput(IEnumerable<ErrorResponse> errors)
        {
            Errors = errors;
        }

        public UseCaseOutput(ErrorResponse error)
        {
            Errors = new[] { error };
        }

        public T? Data { get; }

        public IEnumerable<ErrorResponse>? Errors { get; }

        [JsonIgnore]
        public bool HasErrors
        {
            get
            {
                return Errors != null && Errors.Any();
            }
        }
    }
}
