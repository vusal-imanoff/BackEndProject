using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class SocialAddressController : Controller
    {
        private readonly AppDbContext _context;
        public SocialAddressController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(bool? status)
        {
            IQueryable<SocialAddress> query = _context.SocialAddresses;
            if (status!=null)
            {
                query = query.Where(c => c.IsDeleted == status);
            }
            ViewBag.Status = status;
            return View(await query.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SocialAddress socialAddress)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _context.SocialAddresses.AnyAsync(s=>s.Name.ToLower()==socialAddress.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {socialAddress.Name} Already Exists.");
                return View();
            }
            socialAddress.Name = socialAddress.Name.Trim();
            await _context.SocialAddresses.AddAsync(socialAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            SocialAddress social = await _context.SocialAddresses.FirstOrDefaultAsync(s => s.Id == id);
            if (social==null)
            {
                return NotFound();
            }
            return View(social);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, SocialAddress socialAddress)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id==null)
            {
                return BadRequest();
            }
            if (socialAddress==null)
            {
                return BadRequest();
            }
            SocialAddress dbsocialaddress = await _context.SocialAddresses.FirstOrDefaultAsync(s => s.Id == socialAddress.Id);
            if (dbsocialaddress==null)
            {
                return NotFound();
            }
            if (await _context.SocialAddresses.AnyAsync(s=>s.Id!=socialAddress.Id && s.Name.ToLower() == socialAddress.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {socialAddress.Name} Already Exists.");
                return View();
            }
            if (dbsocialaddress.Name.ToLower()==socialAddress.Name.Trim().ToLower())
            {
                RedirectToAction("index");
            }
            dbsocialaddress.Name = socialAddress.Name.Trim();
            await _context.SaveChangesAsync();
            return RedirectToAction("index");

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            SocialAddress social = await _context.SocialAddresses.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
            social.IsDeleted = true;
            await _context.SaveChangesAsync();

            return PartialView("_SocialAddressIndexPartial", await _context.SocialAddresses.ToListAsync());
        }
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            SocialAddress social = await _context.SocialAddresses.FirstOrDefaultAsync(s => s.Id == id && s.IsDeleted);
            social.IsDeleted = false;
            await _context.SaveChangesAsync();

            return PartialView("_SocialAddressIndexPartial", await _context.SocialAddresses.ToListAsync());
        }

    }
}
