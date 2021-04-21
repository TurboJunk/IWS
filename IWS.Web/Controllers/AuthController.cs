using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using IWS.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IWS.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IOptions<LoginModel> testUser;

        public AuthController(IOptions<LoginModel> testUser)
        {
            this.testUser = testUser;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = testUser.Value;
                if (user.Login == model.Login && user.Password == model.Password)
                {
                    await Authenticate(model.Login);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var i = await HttpContext.AuthenticateAsync();
            var c = i.Succeeded;
            return RedirectToAction("Login", "Auth");
        }
    }
}
