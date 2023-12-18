namespace WhatsUpGoldRemotePoller.Controllers;

[Route("[controller]")]
[ApiController]
[EnableRateLimiting("fixed")]
public class TcpController : ControllerBase
{
    private readonly TcpService _tcpService;

    public TcpController(TcpService tcpService)
    {
        _tcpService = tcpService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string address, string port)
    {
        var result = await _tcpService.TcpAsync(address, port);
        return Ok(result);
    }
}
