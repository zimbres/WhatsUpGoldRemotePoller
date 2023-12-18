namespace WhatsUpGoldRemotePoller.Controllers;

[Route("[controller]")]
[ApiController]
[EnableRateLimiting("fixed")]
public class HttpController : ControllerBase
{
    private readonly HttpService _httpService;

    public HttpController(HttpService httpService)
    {
        _httpService = httpService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string address)
    {
        var result = await _httpService.HttpAsync(address);
        return Ok(result);
    }
}
