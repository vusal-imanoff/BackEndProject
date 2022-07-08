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
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        public BrandController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //IQueryable<Brand> query = _context.Brands.Include(b => b.Products);
            //if (status != null)
            //{
            //    query = query.Where(b => b.IsDeleted == status);
            //}
            //return View(await query.ToArrayAsync());
            return View(await _context.Brands.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            if (await _context.Brands.AnyAsync(b=>b.Name.ToLower()==brand.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {brand.Name} Already Exists");
                return View();
            }
            brand.Name = brand.Name.Trim();
            brand.Createdat = DateTime.UtcNow.AddHours(4);
            await _context.Brands.AddAsync(brand);
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
            Brand brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id==null)
            {
                return BadRequest();
            }
            if (brand.Id!=id)
            {
                return BadRequest();
            }
            Brand dbbrand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == brand.Id);
            if (dbbrand==null)
            {
                return NotFound();
            }
            if (await _context.Brands.AnyAsync(b => b.Id != brand.Id && b.Name.ToLower() == brand.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {brand.Name} Already Exists");
                return View();
            }

            if(dbbrand.Name.ToLower()==brand.Name.Trim().ToLower())
            {
                RedirectToAction("index");
            }
            dbbrand.Name = brand.Name.Trim();
            dbbrand.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Delete(int? id,bool? status)
        {
            
            if (id==null)
            {
                return BadRequest();
            }
            Brand brand = await _context.Brands.FirstOrDefaultAsync(b => !b.IsDeleted && b.Id == id);
            if (brand==null)
            {
                return NotFound();
            }
            brand.IsDeleted = true;
            brand.DeletedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            IQueryable<Brand> query = _context.Brands.Include(b => b.Products);
            if (status!=null)
            {
                query = query.Where(b => b.IsDeleted == status);
            }

            return PartialView("_BrandIndexPartial",await query.ToListAsync());
        }

        public async Task<IActionResult> Restore(int? id, bool? status)
        {
            if (id==null)
            {
                return BadRequest();
            }

            Brand brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted);

            if (brand == null)
            {
                return NotFound();
            }
            brand.IsDeleted = false;
            brand.DeletedAt = null;

            await _context.SaveChangesAsync();

            IQueryable<Brand> query = _context.Brands.Include(b => b.Products);
            if (status!=null)
            {
                query = query.Where(q => q.IsDeleted == status);
            }

            return PartialView("_BrandIndexPartial", await query.ToListAsync());
        }
    }
}
