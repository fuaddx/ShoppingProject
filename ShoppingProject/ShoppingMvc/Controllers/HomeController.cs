﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingMvc.Contexts;
using ShoppingMvc.Models;
using ShoppingMvc.Models.Identity;
using ShoppingMvc.ViewModels.BasketVm;
using ShoppingMvc.ViewModels.HomeVms;
using ShoppingMvc.ViewModels.ProductVm;
using ShoppingMvc.ViewModels.SliderVm;

namespace ShoppingMvc.Controllers
{
    public class HomeController : Controller
    {
        EvaraDbContext _db { get; set; }

        public HomeController(EvaraDbContext db)
        {
            _db = db;
        }

        private async Task<AppUser> GetAuthenticatedUserAsync()
         => await _db.Users
            .Include(u => u.Baskets)
            .ThenInclude(b => b.BasketItems)
            .ThenInclude(b => b.Product)
            .ThenInclude(p => p.ProductImages)
            .FirstOrDefaultAsync(u => u.UserName == HttpContext.User.Identity.Name);


        public async Task<IActionResult> Index()
            {
            HomeVm vm = new HomeVm
            {
                SliderListItems = await _db.Sliders.Select(c => new SliderListItemVm
                {
                    Id = c.Id,
                    CreatedTime = c.CreatedTime,
                    UpdatedTime = c.UpdatedTime,
                    ImageUrl = c.ImageUrl,
                    IsDeleted = c.IsDeleted,
                    Title = c.Title,
                    Description = c.Description,
                    Discount = c.Discount,
                    Button = c.Button,
                }).ToListAsync(),
                ProductListItems = await _db.Products
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages)
                    .Include(p => p.Tags)
                    .Include(p=>p.SellerData)
                    .Where(p=>p.SellerData.isBanned==false)
                    .Where(p => !p.IsDeleted)
                    .Where(p => !p.IsArchived)
                    .Select(p => p.FromProduct_ToProductListItemVm())
                    .ToListAsync(),
            };

            return View(vm);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }



    }
}
