var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRateLimiter(_ => _
.AddFixedWindowLimiter(policyName: "fixed", options =>
{
    options.PermitLimit = 100;
    options.Window = TimeSpan.FromSeconds(10);
    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    options.QueueLimit = 2;
}));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("IgnoreSSL")
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
    };
});
builder.Services.AddSingleton<PingService>();
builder.Services.AddSingleton<TcpService>();
builder.Services.AddSingleton<HttpService>();
builder.Services.AddSingleton<CertificateService>();
builder.Services.AddSingleton<AppSettings>();

var app = builder.Build();

app.UseRateLimiter();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
