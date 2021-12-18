using Factory.Core;
using Factory.Core.Domain.Abstract;
using Factory.Core.Domain.Entities;
using Factory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork DB;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(IUnitOfWork db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            DB = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel accountModel)
        {
            if (ModelState.IsValid == false)
            {
                return Content("Model is invalid");
            }

            User user = DB.UserRepository.Get(accountModel.Username);

            if (user != null)
            {
                var passwordIsOk = await userManager.CheckPasswordAsync(user, accountModel.Password);

                if (passwordIsOk == false)
                {
                    return Content("Username or password is incorrect");
                }
            }
            else
            {
                return Content("Username or password is incorrect");
            }

            var result = await signInManager.PasswordSignInAsync(accountModel.Username, accountModel.Password, true, true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                return Content("Username or password is incorrect");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
