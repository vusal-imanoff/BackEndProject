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
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int pageIndex=1)
        {
            List<Product> products = await _context.Products.ToListAsync();

            if (products==null)
            {
                return NotFound();
            }

            ViewBag.ProductCount = (int)Math.Ceiling((decimal)products.Count/9);
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageIndexCount = 3;
            return View(products.OrderByDescending(p => p.Id).Skip((pageIndex - 1) * 9).Take(9).ToList());
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }

            Product product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Gender)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductTags).ThenInclude(p => p.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }
        public async Task<IActionResult> ProductModal(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Product product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Gender)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductTags).ThenInclude(p => p.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return PartialView("_ProductModalPartial", product);

        }

        public async Task<IActionResult> Search(string search)
        {
            List<Product> products = await _context.Products
                .Where(p => p.Name.Trim().ToLower().Contains(search.Trim().ToLower()) ||
                p.Brand.Name.Trim().ToLower().Contains(search.Trim().ToLower())).OrderByDescending(p => p.Id).Take(4).ToListAsync();

            return PartialView("_SearchPartial", products);
        }
    }
}
