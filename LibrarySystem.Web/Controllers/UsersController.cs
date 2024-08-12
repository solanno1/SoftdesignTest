using LibrarySystem.Services.Interfaces;
using LibrarySystem.Web.ViewModels;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace LibrarySystem.Web.Controllers
{
    [AllowAnonymous]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UsersController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isValid = _userService.ValidateUser(model.Username, model.Password);
                if (isValid)
                {
                    var token = _authService.GenerateJwtToken(model.Username);
                    Response.Cookies.Add(new HttpCookie("AuthToken", token)
                    {
                        HttpOnly = true,
                        Secure = false
                    });
                    return RedirectToAction("Index", "Books");
                }
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.RegisterUser(model.Username, model.Password);
                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            if (Request.Cookies["AuthToken"] != null)
            {            
                var cookie = new HttpCookie("AuthToken", "")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    HttpOnly = true,
                    Secure = true
                };
            Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Login", "Users");
        }        
    }
}