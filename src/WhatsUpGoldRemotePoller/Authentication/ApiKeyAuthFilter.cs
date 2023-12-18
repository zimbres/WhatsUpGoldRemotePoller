namespace WhatsUpGoldRemotePoller.Authentication;

public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly AppSettings _appSettings;

    public ApiKeyAuthFilter(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (_appSettings.AuthEnabled)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var extractedAuthToken))
            {
                context.Result = new UnauthorizedObjectResult("Authorization token was not provided");
                return;
            }

            var authToken = _appSettings.Authorization;

            if (!authToken.Equals(extractedAuthToken))
            {
                context.Result = new UnauthorizedObjectResult("Unauthorized client");
                return;
            }
        }
    }
}
