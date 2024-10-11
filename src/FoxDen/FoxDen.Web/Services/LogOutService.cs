using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace FoxDen.Web.Services
{
    public sealed class LogOutService
    {
        public async Task LogOutAsync(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await context.SignOutAsync(GoogleDefaults.AuthenticationScheme);
            await context.SignOutAsync(DiscordAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
