using LibrarySystem.Services.Interfaces;
using LibrarySystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isValid = _userService.ValidateUser(model.Username, model.Password);
                if (isValid)
                {
                    return RedirectToAction("Index", "Books");
                }
                ModelState.AddModelError("", "Login inválido. Verifique seu nome de usuário e senha.");
            }

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
    }
}