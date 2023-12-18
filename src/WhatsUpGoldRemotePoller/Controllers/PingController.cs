namespace WhatsUpGoldRemotePoller.Controllers;

[Route("[controller]")]
[ApiController]
[EnableRateLimiting("fixed")]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
public class PingController : ControllerBase
{
    private readonly PingService _pingService;

    public PingController(PingService pingService)
    {
        _pingService = pingService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery, Required] string address)
    {
        var result = await _pingService.PingAsync(address);
        return Ok(result);
    }
}
