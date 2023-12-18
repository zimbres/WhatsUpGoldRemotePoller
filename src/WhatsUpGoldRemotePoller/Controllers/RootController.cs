namespace WhatsUpGoldRemotePoller.Controllers;

[Route("/")]
[ApiController]
[EnableRateLimiting("fixed")]
public class RootController : ControllerBase
{
    [HttpGet]
    public ActionResult<RootModel> Get()
    {
        var rootModel = new RootModel();
        return Ok(rootModel);
    }
}
