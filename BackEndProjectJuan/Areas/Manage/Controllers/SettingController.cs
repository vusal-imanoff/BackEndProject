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

    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(bool? status)
        {
            IQueryable<Setting> query = _context.Settings;
            if (status != null)
            {
                query = query.Where(b => b.IsDeleted == status);
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
        public async Task<IActionResult> Create(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _context.Settings.AnyAsync(s=> s.Key.ToLower()==setting.Key.Trim().ToLower()))
            {
                ModelState.AddModelError("Key", $"This {setting.Key} Already Exists");
                return View();
            }
            setting.Key = setting.Key.Trim();
            setting.Value = setting.Value.Trim();
            await _context.Settings.AddAsync(setting);
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
            Setting setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (setting==null)
            {
                return NotFound();
            }
            return View(setting);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id==null)
            {
                return BadRequest();
            }
            if (setting==null)
            {
                return BadRequest();
            }
            Setting dbsetting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == setting.Id);
            if (dbsetting==null)
            {
                return NotFound();
            }
            if (await _context.Settings.AnyAsync(c=>c.Id!=setting.Id&&c.Key.ToLower()==setting.Key.Trim().ToLower()))
            {
                ModelState.AddModelError("Key", $"This {setting.Key} Already Exists");
                return View();
            }
            if (dbsetting.Key.ToLower()==setting.Key.Trim().ToLower())
            {
                return View();
            }
            dbsetting.Key = setting.Key.Trim();
            dbsetting.Value = setting.Value.Trim();
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            Setting setting = await _context.Settings.FirstOrDefaultAsync(s => !s.IsDeleted && s.Id == id);
            setting.IsDeleted = true;
            await _context.SaveChangesAsync();

            return PartialView("_SettingIndexPartial", await _context.Settings.ToListAsync());
        }

        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Setting setting = await _context.Settings.FirstOrDefaultAsync(s => s.IsDeleted && s.Id == id);
            setting.IsDeleted = false;
            await _context.SaveChangesAsync();

            return PartialView("_SettingIndexPartial", await _context.Settings.ToListAsync());
        }
    }
}
