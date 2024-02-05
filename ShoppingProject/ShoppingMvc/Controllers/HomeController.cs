using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingMvc.Contexts;
using ShoppingMvc.Models;
using ShoppingMvc.ViewModels.HomeVm;
using ShoppingMvc.ViewModels.SliderVm;
using System.Diagnostics;

namespace ShoppingMvc.Controllers
{
    public class HomeController : Controller
    {
        EvaraDbContext _db {  get; set; }

        public HomeController(EvaraDbContext db)
        {
            _db = db;
        }

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
                }).ToListAsync()
            };
            return View(vm);
        }

    }
}
