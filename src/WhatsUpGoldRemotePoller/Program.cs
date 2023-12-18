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
builder.Services.AddSwaggerGen(s =>
{
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter the token with the `Bearer` prefix, e.g. \"Bearer a1b2c3d4\"",
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme",
    });
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
    {
        { scheme, new List<string>() }
    };
    s.AddSecurityRequirement(requirement);
});

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

builder.Services.AddScoped<ApiKeyAuthFilter>();

var app = builder.Build();

app.UseRateLimiter();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
