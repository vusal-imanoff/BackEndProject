using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Areas.Manage.Controllers
{
    [Area("manage")]
    public class GenderController : Controller
    {
        private readonly AppDbContext _context;
        public GenderController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(bool? status)
        {
            IQueryable<Gender> query = _context.Genders.Include(g => g.Products);
            if (status != null)
            {
                query = query.Where(g => g.IsDeleted == status);
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
        public async Task<IActionResult> Create(Gender gender)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _context.Genders.AnyAsync(g => g.Name.ToLower() == gender.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {gender.Name} Already Exists");
                return View();
            }
            gender.Name = gender.Name.Trim();
            await _context.Genders.AddAsync(gender);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Gender gender = await _context.Genders.FirstOrDefaultAsync(g => g.Id == id);
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Gender gender)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null)
            {
                return BadRequest();
            }
            if (gender.Id != id)
            {
                return BadRequest();
            }
            Gender dbgender = await _context.Genders.FirstOrDefaultAsync(g => g.Id == gender.Id);
            if (dbgender == null)
            {
                return NotFound();
            }
            if (await _context.Genders.AnyAsync(g => g.Id != gender.Id && g.Name.ToLower() == gender.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {gender.Name} Already Exists");
                return View();
            }
            if (dbgender.Name.ToLower() == gender.Name.Trim().ToLower())
            {
                RedirectToAction("index");
            }
            dbgender.Name = gender.Name.Trim();
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id, bool? status)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Gender gender = await _context.Genders.FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted);
            if (gender == null)
            {
                return NotFound();
            }
            gender.IsDeleted = true;
            await _context.SaveChangesAsync();
            IQueryable<Gender> query = _context.Genders.Include(b => b.Products);

            if (status != null)
            {
                query = query.Where(b => b.IsDeleted == status);
            }

            ViewBag.Status = status;
            return PartialView("_GenderIndexPartial", await query.ToListAsync());
        }
        public async Task<IActionResult> Restore(int? id, bool? status)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Gender gender = await _context.Genders.FirstOrDefaultAsync(g => g.Id == id && g.IsDeleted);
            if (gender == null)
            {
                return NotFound();
            }
            gender.IsDeleted = false;
            await _context.SaveChangesAsync();
            IQueryable<Gender> query = _context.Genders.Include(b => b.Products);

            if (status != null)
            {
                query = query.Where(b => b.IsDeleted == status);
            }

            ViewBag.Status = status;
            return PartialView("_GenderIndexPartial", await query.ToListAsync());
        }
    }
}
