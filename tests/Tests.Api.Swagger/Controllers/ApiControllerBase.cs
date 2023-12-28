using Microsoft.AspNetCore.Mvc;

namespace Tests.Api.Swagger.Controllers
{
    /// <summary>
    /// Controlador base para los controladores.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        
    }
}
