namespace WhatsUpGoldRemotePoller.Services;

public class TcpService
{
    private readonly ILogger<TcpService> _logger;

    public TcpService(ILogger<TcpService> logger)
    {
        _logger = logger;
    }

    public async Task<bool> TcpAsync(string address, string port)
    {
        TcpClient tcpClient = new();

        try
        {
            await tcpClient.ConnectAsync(address, int.Parse(port));
        }
        catch (Exception ex)
        {
            _logger.LogWarning("{ex}", ex.Message);
        }

        if (tcpClient.Connected)
        {
            tcpClient.Dispose();
            return true;
        }

        tcpClient.Dispose();
        return false;
    }
}
