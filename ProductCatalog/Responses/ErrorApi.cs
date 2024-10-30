using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Responses
{
    public class ErrorApi
    {
        public ErrorApi(string traceId, IEnumerable<ErrorResponse> erros)
        {
            Errors = erros;
            TraceId = traceId;
        }

        public string TraceId { get; }

        public DateTime IssueDate => DateTime.UtcNow;
        
        public IEnumerable<ErrorResponse> Errors { get; }
    }
}