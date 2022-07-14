using BackEndProjectJuan.Models;
using BackEndProjectJuan.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Areas.Manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin")]

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }
       
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("home", "dashboard", new { area = "manage" });
            }
            else
            {
                return RedirectToAction("login", "account", new { area = "manage" });

            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = await _userManager.FindByEmailAsync(loginVM.Email);

            if (appUser==null)
            {
                ModelState.AddModelError("", "Email or Password is Incorrect");
                return View();
            }
            if (!await _userManager.CheckPasswordAsync(appUser,loginVM.Password))
            {
                ModelState.AddModelError("", "Email or Password is Incorrect");
                return View();
            }
            await _signManager.PasswordSignInAsync(appUser, loginVM.Password, loginVM.Remindme, true);

            return RedirectToAction("index", "dashboard", new { area = "manage" });
        }
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("login", "account", new { area = "manage" });
        }
    }
}
