﻿using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Enums;
using BackEndProjectJuan.Models;
using BackEndProjectJuan.ViewModels;
using BackEndProjectJuan.ViewModels.Basket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Controllers
{
    [Authorize(Roles = "Member")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            string basket = Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;

            if (!string.IsNullOrWhiteSpace(basket))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

                foreach (BasketVM basketVM in basketVMs)
                {
                    Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketVM.Id && !p.IsDeleted);

                    basketVM.Name = product.Name;
                    basketVM.Extax = product.Extax;
                    basketVM.Price = product.DiscountPrice > 0 ? product.DiscountPrice : product.Price;
                }
            }
            else
            {
                return BadRequest();
            }

            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            OrderVM orderVM = new OrderVM
            {
                Order = new Order
                {
                    Name = appUser.Name,
                    SurName = appUser.Surname,
                    Email = appUser.Email
                },
                BasketVMs = basketVMs
            };

            return View(orderVM);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            string basket = Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;

            if (!string.IsNullOrWhiteSpace(basket))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                List<OrderItem> orderItems = new List<OrderItem>();

                foreach (BasketVM basketVM in basketVMs)
                {
                    Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketVM.Id && !p.IsDeleted);

                    OrderItem orderItem = new OrderItem
                    {
                        Price = product.DiscountPrice > 0 ? product.DiscountPrice : product.Price,
                        Count = basketVM.Count,
                        ProductId = product.Id,
                        TotalPrice = basketVM.Count * (product.DiscountPrice > 0 ? product.DiscountPrice : product.Price)
                    };

                    orderItems.Add(orderItem);
                }

                order.OrderItems = orderItems;
            }
            else
            {
                return BadRequest();
            }

            order.OrderStatus = OrderStatus.Pending;
            order.Date = DateTime.UtcNow.AddHours(4);
            order.AppUserId = appUser.Id;
            order.TotalPrice = order.OrderItems.Sum(x => x.TotalPrice);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            Response.Cookies.Delete("basket");

            return RedirectToAction("index", "home");
        }
    }
}
