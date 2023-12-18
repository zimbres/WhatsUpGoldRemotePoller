namespace WhatsUpGoldRemotePoller.Services;

public class HttpService
{
    private readonly ILogger<HttpService> _logger;
    private readonly HttpClient _httpClient;

    public HttpService(ILogger<HttpService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("IgnoreSSL");
    }
    public async Task<bool> HttpAsync(string address)
    {
        HttpResponseMessage result = null;

        try
        {
            result = await _httpClient.GetAsync(address);
            _ = await result.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogWarning("{ex}", ex.Message);
        }

        if (result is not null && result.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }
}
