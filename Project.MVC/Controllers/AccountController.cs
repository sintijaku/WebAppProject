using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Project.MODEL.Entities;
using Project.MVC.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Project.MVC.Controllers
{
    public class AccountController : Controller
    {
        private IAppUserService _service;

        public AccountController(IAppUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username)
        {
            AppUser user = _service.GetMyUser(username);

            if (user == null || !ModelState.IsValid)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (_service.UsernameExisits(user))
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
                    identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                }
            }
            return RedirectToAction("index", "user");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AppUser item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                if (_service.UsernameExisits(item))
                {
                    ViewBag.UserExists = "This user is already registered";
                    return View();
                }

                item.Password = Crypto.HashPassword(item.Password);
                _service.Add(item);

                return RedirectToAction("index");
            }
            return RedirectToAction("index", "user");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login", "account");
        }

    }
}