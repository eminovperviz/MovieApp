namespace MovieApp.Api.Controllers;

/// <summary>
/// Base controller
/// </summary>
[Route("api/v{v:apiVersion}/[controller]")]
[Route("api/{culture}/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class BaseController : ControllerBase
{
    [HttpGet("Test")]
    public ActionResult Test()
    {
        return Ok("Worked !");
    }
}
