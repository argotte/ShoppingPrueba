using Microsoft.AspNetCore.Mvc;
using ShoppingPrueba.Helpers;
using ShoppingPrueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingPrueba.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
     //   private readonly IUserHelper _userHelper;
        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult login() 
        {
            if (User.Identity.IsAuthenticated)  // esta logeado?
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            }

            return View(model);
        }


        public async Task<IActionResult> logout() 
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");

        }

        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}
