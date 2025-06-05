using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Testerke.Idp;
using Testerke.Idp.Authentication;
using Testerke.Idp.Components;
using Testerke.Idp.Components.Account;
using Testerke.Idp.Data;
using Testerke.Idp.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Aspire
builder.AddServiceDefaults();

// Blazor
builder.Services.AddRazorComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();

// EF
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("idpDb")));
builder.EnrichNpgsqlDbContext<AppDbContext>();

// Identity
builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddInMemoryClients([
        new Client
        {
            ClientId = "web",
            ClientSecrets = [new Secret("secret".Sha256())],
            AllowedGrantTypes = GrantTypes.Code,
            AllowedScopes = [],
            AllowOfflineAccess = true,
            RedirectUris = [$"{Environment.GetEnvironmentVariable("WEB_HTTPS")}/signin-oidc"],
            PostLogoutRedirectUris = [$"{Environment.GetEnvironmentVariable("WEB_HTTPS")}/signout-callback-oidc"],
            RequirePkce = true
        }
    ])
    .AddInMemoryIdentityResources([
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new IdentityResource("color", ["favorite_color"])
    ])
    .AddAspNetIdentity<AppUser>()
    .AddProfileService<CustomUserProfileService>();

// AuthN & AuthZ
builder.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseIdentityServer();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>();

app.MapAdditionalIdentityEndpoints();
app.MapDefaultEndpoints();

app.Run();