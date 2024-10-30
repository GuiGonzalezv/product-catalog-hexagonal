using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Interfaces;
using System.Net;

namespace ProductCatalog.Responses
{
    public class Presenter : IPresenter
    {
        private readonly ContentResult _contentResult;
        private bool _hasErrors;
        private readonly IHttpContextAccessor _httpContextAccessor; 


        public Presenter(IHttpContextAccessor httpContextAccessor)
        {
            _contentResult = new JsonContentResult();
            _httpContextAccessor = httpContextAccessor;
        }

        public void Handle<T>(UseCaseOutput<T> output) where T : class
        {
            _hasErrors = output.HasErrors;
            var noContent = output.Data == null && !_hasErrors;

            if (noContent)
                return;

            SetContentResult(output);
        }

        private void SetContentResult<T>(UseCaseOutput<T> output) where T : class
        {
            var traceId = _httpContextAccessor.HttpContext.TraceIdentifier;

            _contentResult.Content = _hasErrors
                ? JsonSerializer.SerializeObject(new ErrorApi(traceId, output.Errors))
                : JsonSerializer.SerializeObject(output.Data);
        }

        private ContentResult SetStatusCode(HttpStatusCode successStatus, HttpStatusCode invalidStatus)
        {
            var status = _hasErrors ? invalidStatus : successStatus;
            if (!_hasErrors && _contentResult.Content == null)
                _contentResult.StatusCode = (int)HttpStatusCode.NoContent;
            else
                _contentResult.StatusCode = (int)status;
            return _contentResult;
        }

        public virtual ActionResult ResultForPost(HttpStatusCode successStatus)
        {
            return SetStatusCode(successStatus, HttpStatusCode.BadRequest);
        }

        public virtual ActionResult ResultForPut(HttpStatusCode successStatus)
        {
            return SetStatusCode(successStatus, HttpStatusCode.BadRequest);
        }

        public virtual ActionResult ResultForGet()
        {
            if (_contentResult.Content == null)
                return new NotFoundResult();

            _contentResult.StatusCode = (int)HttpStatusCode.OK;
            return _contentResult;
        }

        public virtual ActionResult ResultForDelete(HttpStatusCode successStatus)
        {
            return SetStatusCode(successStatus, HttpStatusCode.BadRequest);
        }
    }
}