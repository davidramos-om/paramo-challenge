using Microsoft.AspNetCore.Mvc;

namespace Sat.Recruitment.Api.Controllers
{
    /// <summary>
    /// Use consistent route convention and the [ApiController] attribute
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }

}
