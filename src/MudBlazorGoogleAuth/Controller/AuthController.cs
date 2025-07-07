using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MudBlazorGoogleAuth.Controller;

[Route("api/[controller]")]
public class AuthController : Microsoft.AspNetCore.Mvc.Controller
{
    [HttpGet("login-google")]
    public IActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Content("~/") };
        return Challenge(properties, "Google");
    }
}