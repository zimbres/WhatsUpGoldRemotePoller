namespace WhatsUpGoldRemotePoller.Services;

public class AppSettings
{
    private readonly Configurations _configuration;

    public string CertificateExpiration { get; set; }

    public AppSettings(IConfiguration configuration)
    {
        _configuration = configuration.GetSection(nameof(Configurations)).Get<Configurations>();

        _ = bool.TryParse(Environment.GetEnvironmentVariable("UseEnvironmentVariables"), out bool useEnv);

        CertificateExpiration = Environment.GetEnvironmentVariable("CertificateExpiration") is not null && useEnv
            ? Environment.GetEnvironmentVariable("CertificateExpiration")
            : _configuration.CertificateExpiration;
    }
}
