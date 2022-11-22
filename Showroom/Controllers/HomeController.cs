using NToastNotify;
using Showroom.Core.Interfaces;
using Showroom.Core.ViewModels.Home;
using Showroom.Extensions;
using Showroom.Filters;

namespace Showroom.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IToastNotification toastNotification)
        {
            _logger = logger;
            _userService = userService;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        [AuthenticationFilter]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loggedUser = _userService.Login(model);

            if (loggedUser == null)
            {
                this.ModelState.AddModelError("authError", "Invalid username or password!");
                return View(model);
            }

            HttpContext.Session.SetObject("loggedUser", loggedUser);

            _toastNotification.AddSuccessToastMessage("Successfully login!");
            return RedirectToAction("Index", "Home");
        }

        [AuthenticationFilter]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("loggedUser");

            _toastNotification.AddSuccessToastMessage("Successfully logout!");
            return RedirectToAction("Login", "Home");
        }
    }
}