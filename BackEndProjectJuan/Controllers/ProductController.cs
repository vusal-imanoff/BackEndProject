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
        public IActionResult Index()
        {
            return View();
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
