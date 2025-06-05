using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace Testerke.Idp.Authentication;

internal static class AuthenticationExtensions
{
    public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
    {
        var authenticationBuilder = builder.Services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = IdentityConstants.ApplicationScheme;
                opts.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.LoginPath = "/Account/Login";
                options.Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
                };
            })
            .AddCookie(IdentityConstants.ExternalScheme, options =>
            {
                options.Cookie.Name = IdentityConstants.ExternalScheme;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });

        var externalProviders = new Dictionary<string, ExternalAuthProvider>
        {
            ["Google"] = static (builder, configure) => builder.AddGoogle(configure)
        };

        foreach (var (providerName, provider) in externalProviders)
        {
            var section = builder.Configuration.GetSection($"Authentication:Schemes:{providerName}");
            if (section.Exists())
                provider(authenticationBuilder, options =>
                {
                    section.Bind(options);
                    if (options is RemoteAuthenticationOptions remoteAuthenticationOptions)
                        remoteAuthenticationOptions.SignInScheme = IdentityConstants.ExternalScheme;
                });
        }

        return builder;
    }

    private delegate void ExternalAuthProvider(AuthenticationBuilder builder, Action<object> configure);
}