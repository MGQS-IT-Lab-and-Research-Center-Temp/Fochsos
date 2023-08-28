using Fochso.Models;
using Fochso.Models.Authorize;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Fochso.Service.Interface;
using AspNetCoreHero.ToastNotification.Abstractions;
using Fochso.ActionFilters;
using Microsoft.EntityFrameworkCore;
using Fochso.Models.Class;
using Fochso.Context;

namespace Fochso.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly INotyfService _notyf;
        private readonly FochsoContext _context;


        public HomeController(ILogger<HomeController> logger,IUserService userService,INotyfService notyfService, FochsoContext context)
        {
            _logger = logger;
            _userService = userService;
            _notyf = notyfService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            var response = _userService.AddUser(model);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);

                return View(model);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Login", "Home");
        }

        [RedirectIfAuthenticated]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var response = _userService.Login(model);
            var user = response.Data;

            if (response.Status == false)
            {
                _notyf.Error(response.Message);

                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.UserName),
                //new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleName),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authenticationProperties = new AuthenticationProperties();

            var principal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

            _notyf.Success(response.Message);

            if (user.UserName  == "Admin")
            {
                return RedirectToAction("AdminDashboard", "Home");
            }

            if (User.Identity.IsAuthenticated)
            {
                // Redirect to the home page.
                RedirectToAction("Index","Home");
            }


            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _notyf.Success("You have successfully signed out!");
            return RedirectToAction("Login", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            return View();
        }
        public async Task<ActionResult> About()
        {
            IQueryable<DateCreatedGroup> data =
                from className in _context.Classes
                group className by className.DateCreated into dateGroup
                select new DateCreatedGroup()
                {
                    DateCreated = dateGroup.Key,
                    ClassCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }
    }
}