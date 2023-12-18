namespace WhatsUpGoldRemotePoller.Controllers;

[Route("[controller]")]
[ApiController]
[EnableRateLimiting("fixed")]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
public class CertificateController : ControllerBase
{
    private readonly CertificateService _certificateService;

    public CertificateController(CertificateService certificateService)
    {
        _certificateService = certificateService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery, Required] string address)
    {
        var result = await _certificateService.CertificateAsync(address);
        return Ok(result);
    }
}
