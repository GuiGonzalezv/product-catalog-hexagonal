using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Interfaces;
using System.Net.Mime;
using System.Net;

namespace ProductCatalog.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class BaseController : ControllerBase
    {
        private readonly IPresenter _presenter;

        public BaseController(IPresenter presenter)
        {
            _presenter = presenter;
        }

        protected ActionResult ResultForPost(HttpStatusCode successStatus)
        {
            return _presenter.ResultForPost(successStatus);
        }

        protected ActionResult ResultForPut(HttpStatusCode successStatus)
        {
            return _presenter.ResultForPut(successStatus);
        }

        protected ActionResult ResultForGet()
        {
            return _presenter.ResultForGet();
        }

        protected ActionResult ResultForDelete(HttpStatusCode successStatus)
        {
            return _presenter.ResultForDelete(successStatus);
        }
    }
}
