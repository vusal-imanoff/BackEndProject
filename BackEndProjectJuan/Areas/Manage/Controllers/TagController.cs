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

    public class TagController : Controller
    {
        private readonly AppDbContext _context;
        public TagController(AppDbContext context)
        {
            _context=context;
        }
        public async Task<IActionResult> Index(bool? status)
        {
            IQueryable<Tag> query = _context.Tags;
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
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _context.Tags.AnyAsync(t=>t.Name.ToLower()==tag.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {tag.Name} Already Exists");
                return View();
            }
            tag.Name = tag.Name.Trim();
            await _context.Tags.AddAsync(tag);
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
            Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if (tag==null)
            {
                return NotFound();
            }
            return View(tag);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id==null)
            {
                return BadRequest();
            }
            if (tag.Id!=id)
            {
                return BadRequest();
            }
            Tag dbtag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if (dbtag==null)
            {
                return NotFound();
            }
            if (await _context.Tags.AnyAsync(t => t.Id == tag.Id && t.Name.ToLower() == tag.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {tag.Name} Already Exists");
                return View();
            }
            if (dbtag.Name.ToLower()==tag.Name.Trim().ToLower())
            {
                return View();
            }
            dbtag.Name = tag.Name.Trim();
            await _context.SaveChangesAsync();  

            return RedirectToAction("index");

        }

        public async Task<IActionResult> Delete(int? id, bool? status)
        {

            if (id == null)
            {
                return BadRequest();
            }
            Tag tag = await _context.Tags.FirstOrDefaultAsync(t => !t.IsDeleted && t.Id == id);
            if (tag == null)
            {
                return NotFound();
            }
            tag.IsDeleted = true;
            await _context.SaveChangesAsync();

            IQueryable<Tag> query = _context.Tags;
            if (status != null)
            {
                query = query.Where(b => b.IsDeleted == status);
            }

            return PartialView("_TagIndexPartial", await query.ToListAsync());
        }

        public async Task<IActionResult> Restore(int? id, bool? status)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.IsDeleted && t.Id == id);
            if (tag == null)
            {
                return NotFound();
            }
            tag.IsDeleted = false;
            await _context.SaveChangesAsync();

            IQueryable<Tag> query = _context.Tags;
            if (status != null)
            {
                query = query.Where(b => b.IsDeleted == status);
            }

            return PartialView("_TagIndexPartial", await query.ToListAsync());
        }
    }
}
