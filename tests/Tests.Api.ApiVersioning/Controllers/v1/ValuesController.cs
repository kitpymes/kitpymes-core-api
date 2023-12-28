using Kitpymes.Core.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Api.ApiVersioning.Controllers.v1
{
    public class ValuesController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(HttpContext.GetRequestedApiVersion());
    }
}
