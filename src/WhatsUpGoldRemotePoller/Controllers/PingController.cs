namespace WhatsUpGoldRemotePoller.Controllers;

[Route("[controller]")]
[ApiController]
[EnableRateLimiting("fixed")]
public class PingController : ControllerBase
{
    private readonly PingService _pingService;

    public PingController(PingService pingService)
    {
        _pingService = pingService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string address)
    {
        var headers = Request.Headers;
        var result = await _pingService.PingAsync(address);
        return Ok(result);
    }
}
