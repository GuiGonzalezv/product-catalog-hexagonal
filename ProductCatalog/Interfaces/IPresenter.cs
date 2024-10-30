using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Interfaces
{
    public interface IPresenter : Domain.Ports.IOutputPort
    {
        ActionResult ResultForPost(HttpStatusCode successStatus);

        ActionResult ResultForPut(HttpStatusCode successStatus);

        ActionResult ResultForDelete(HttpStatusCode successStatus);

        ActionResult ResultForGet();
    }
}