namespace WhatsUpGoldRemotePoller.Services;

public class PingService
{
    private readonly ILogger<PingService> _logger;

    public PingService(ILogger<PingService> logger)
    {
        _logger = logger;
    }

    public async Task<bool> PingAsync(string address)
    {
        Ping ping = new();
        PingReply pingReply = null;

        try
        {
            pingReply = await ping.SendPingAsync(address);
        }
        catch (Exception ex)
        {
            _logger.LogWarning("{ex}", ex.Message);
        }

        if (pingReply?.Status == IPStatus.Success)
        {
            return true;
        }
        return false;
    }
}
