using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public async  Task<IActionResult> Index(int pageindex=1)
        {
            List<Blog> blogs = await _context.Blogs.ToListAsync();

            if (blogs == null)
            {
                return NotFound();
            }

            ViewBag.ProductCount = (int)Math.Ceiling((decimal)blogs.Count / 4);
            ViewBag.PageIndex = pageindex;
            ViewBag.PageIndexCount = 3;
            return View(blogs.OrderByDescending(p => p.Id).Skip((pageindex - 1) * 4).Take(4).ToList());
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Blog blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);

        }
    }
}
