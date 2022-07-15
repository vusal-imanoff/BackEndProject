using BackEndProjectJuan.Areas.Manage.ViewModels.AccountViewModels;
using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public UserController(UserManager<AppUser> userManager,AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index(bool? status)
        {
            List<AppUser> query = await _userManager.Users.ToListAsync();
            if (status != null)
            {
                query = query.Where(q => q.IsDeActive == status).ToList();
            }
            foreach (AppUser user in query)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.Role = roles[0];
            }
            return View(query);
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = new AppUser
            {
                Name = userVM.Name,
                Surname = userVM.SurName,
                FatherName = userVM.FatherName,
                Age = userVM.Age,
                Email = userVM.Email,
                UserName = userVM.UserName
            };
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, userVM.Password);
            if (userVM.IsAdmin==true)
            {
                await _userManager.AddToRoleAsync(appUser, "Admin");
            }
            else
            {
                await _userManager.AddToRoleAsync(appUser, "Member");

            }
            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View();
            }
            return RedirectToAction("index", "user", new { area = "manage" });
        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser==null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string id, ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser==null)
            {
                return NotFound();
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

            await _userManager.ResetPasswordAsync(appUser, token, resetPasswordVM.Password);

            return RedirectToAction("index");

        }

        public async Task<IActionResult> DeActive(string id)
        {
            if (id == null) return BadRequest();

            AppUser appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null) return NotFound();
            appUser.IsDeActive = true;
            
            await _context.SaveChangesAsync();



            return PartialView("_UserIndexPartial", await _userManager.Users.ToListAsync());
        }

        public async Task<IActionResult> Activate(string id)
        {
            if (id == null) return BadRequest();

            AppUser appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null) return NotFound();
            appUser.IsDeActive = false;

            await _context.SaveChangesAsync();



            return PartialView("_UserIndexPartial", await _userManager.Users.ToListAsync());
        }
    }
}
