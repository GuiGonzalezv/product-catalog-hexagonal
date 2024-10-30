using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ProductCatalog.Responses
{
    public class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            ContentType = MediaTypeNames.Application.Json;
        }
    }
}