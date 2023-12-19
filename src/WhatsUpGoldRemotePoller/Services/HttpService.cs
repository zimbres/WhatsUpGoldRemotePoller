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
    public async Task<bool> HttpAsync(string address, string keyword)
    {
        HttpResponseMessage response = null;
        string result = null;

        try
        {
            response = await _httpClient.GetAsync(address);
            result = await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogWarning("{ex}", ex.Message);
        }

        if (response is not null && response.IsSuccessStatusCode)
        {
            if(keyword is not null && !result.Contains(keyword, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }
        return false;
    }
}
