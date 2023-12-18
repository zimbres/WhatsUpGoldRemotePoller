namespace WhatsUpGoldRemotePoller.Controllers;

[Route("[controller]")]
[ApiController]
[EnableRateLimiting("fixed")]
public class CertificateController : ControllerBase
{
    private readonly CertificateService _certificateService;

    public CertificateController(CertificateService certificateService)
    {
        _certificateService = certificateService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string address)
    {
        var result = await _certificateService.CertificateAsync(address);
        return Ok(result);
    }
}
