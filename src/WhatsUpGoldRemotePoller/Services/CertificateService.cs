namespace WhatsUpGoldRemotePoller.Services;

public class CertificateService
{
    private readonly ILogger<CertificateService> _logger;
    private readonly AppSettings _appSettings;

    public CertificateService(ILogger<CertificateService> logger, AppSettings appSettings)
    {
        _logger = logger;
        _appSettings = appSettings;
    }
    public async Task<bool> CertificateAsync(string address)
    {
        DateTime notAfter = DateTime.UtcNow;

        var httpClientHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (request, cert, chain, policyErrors) =>
            {
                notAfter = cert.NotAfter;
                return true;
            }
        };

        var httpClient = new HttpClient(httpClientHandler);

        try
        {
            var result = await httpClient.SendAsync(new HttpRequestMessage(new HttpMethod("Head"), address));

            if (notAfter >= DateTime.UtcNow.AddDays(int.Parse(_appSettings.CertificateExpiration)))
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("{ex}", ex.Message);
        }
        return false;
    }
}
