using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Testerke.Idp.Data.Models;

namespace Testerke.Idp;

internal sealed class CustomUserProfileService(
    UserManager<AppUser> userManager,
    IUserClaimsPrincipalFactory<AppUser> claimsFactory,
    ILogger<ProfileService<AppUser>> logger)
    : ProfileService<AppUser>(userManager, claimsFactory, logger)
{
    protected override Task GetProfileDataAsync(ProfileDataRequestContext context, AppUser user)
    {
        return base.GetProfileDataAsync(context, user);
    }
}