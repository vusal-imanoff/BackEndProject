using BackEndProjectJuan.Interfaces;
using BackEndProjectJuan.Models;
using BackEndProjectJuan.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser appUser = new AppUser
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                FatherName = registerVM.FatherName,
                Age = registerVM.Age,
                Email = registerVM.Email,
                UserName = registerVM.UserName
            };

            IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();

            }

            await _userManager.AddToRoleAsync(appUser, "Member");
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var link = Url.Action(nameof(VerifyEmail), "Account", new { userId = appUser.Id, token = code }, Request.Scheme, Request.Host.ToString());
            string html = $" Click Here and to be conteniou registration {link}";    
            await _emailService.SendEmailAsync(registerVM.Email, html);
            return RedirectToAction(nameof(SendVerifyEmail));

        }
       public IActionResult SendVerifyEmail()
        {
            return View();
        }

        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (userId==null || token==null)
            {
                return BadRequest();
            }
            AppUser appUser = await _userManager.FindByIdAsync(userId);
            if (appUser==null)
            {
                return BadRequest();
            }
            await _userManager.ConfirmEmailAsync(appUser, token);
            await _signInManager.SignInAsync(appUser, false);
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
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
                ModelState.AddModelError("", "Email Or Password Is InCorrect");
                return View();
            }
            if (appUser.IsDeActive)
            {
                ModelState.AddModelError("", "Your Profile is DeActive");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(appUser,loginVM.Password, loginVM.Remindme, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Your Profile Blocked");
                return View();
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email Or Password Is InCorrect");
                return View();
            }

            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

        [HttpGet]
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Profile()
        {

            AppUser app = await _userManager.Users.Include(u=>u.Orders).ThenInclude(u=>u.OrderItems).ThenInclude(u=>u.Product).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            ProfileVM profileVM = new ProfileVM
            {
                Name = app.Name,
                SurName = app.Surname,
                FatherName = app.FatherName,
                Age = app.Age,
                UserName = app.UserName,
                Email = app.Email
            };

            MemberVM memberVM = new MemberVM
            {
                ProfileVM = profileVM,
                Orders = app.Orders.ToList()
            };
            return View(memberVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProfileVM profileVM)
        {
            if (!ModelState.IsValid) return View("Profile");
            AppUser appUser =await _userManager.FindByNameAsync(User.Identity.Name);
            appUser.Name = profileVM.Name;
            appUser.Surname = profileVM.SurName;
            appUser.Email = profileVM.Email;
            appUser.FatherName = profileVM.FatherName;
            appUser.UserName = profileVM.UserName;
            appUser.Age = profileVM.Age;

            IdentityResult identity = await _userManager.UpdateAsync(appUser);

            if (!identity.Succeeded)
            {
                foreach (var item in identity.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View("profile");

            }

            if (profileVM.CurrentPassword!=null)
            {
                if (profileVM.NewPassword==null)
                {
                    ModelState.AddModelError("NewPassword", "Is Requered");
                    ModelState.AddModelError("ConfirmPassword", "Is Requered");

                    return View("Profile");
                }

                if (!await _userManager.CheckPasswordAsync(appUser,profileVM.CurrentPassword))
                {
                    ModelState.AddModelError("CurrentPassword", "Current Pssword Is InCorrect.");
                    return View("profile");
                }
                identity = await _userManager.ChangePasswordAsync(appUser, profileVM.CurrentPassword, profileVM.NewPassword);

                if (!identity.Succeeded)
                {
                    foreach (var item in identity.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                    return View("Profile");
                }

            }
            return RedirectToAction("Profile");
        }
        #region CreateRole

        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });


        //    return Content("Role Created Successfully");
        //}
        #endregion

        #region Create Super Admin
        //public async Task<IActionResult> CreateSuperAdmin()
        //{
        //    AppUser appUser = new AppUser
        //    {
        //        Email = "superadmin@mail.ru",
        //        UserName = "SuperAdmin",
        //        Name = "Vusal",
        //        Surname = "Imanov",
        //        FatherName = "Musfiq",
        //        Age = 22
        //    };

        //    await _userManager.CreateAsync(appUser, "Vusal@123");
        //    await _userManager.AddToRoleAsync(appUser, "SuperAdmin");

        //    return Content("Superadmin Created Successfully");
        //}
        #endregion
    }
}
