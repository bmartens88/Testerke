using Microsoft.AspNetCore.Identity;

namespace Testerke.Idp.Data.Models;

internal sealed class AppUser : IdentityUser
{
    public string FavoriteColor { get; set; } = string.Empty;
}