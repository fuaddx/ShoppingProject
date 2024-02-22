using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingMvc.Contexts;
using ShoppingMvc.ViewModels.CombinedViewModel;
using ShoppingMvc.ViewModels.HomeVms;
using ShoppingMvc.ViewModels.ProductDetailVms;
using ShoppingMvc.ViewModels.ProductVm;
using ShoppingMvc.ViewModels.SliderVm;
using System.Xml.Linq;

namespace ShoppingMvc.ViewComponents.ProductDetail2VIewComponent
{
    [ViewComponent(Name = "ProductDetail")]
    public class ProductDeTailViewComponent : ViewComponent
    {
        EvaraDbContext _db { get; set; }

        public ProductDeTailViewComponent(EvaraDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
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
                   .Include(p => p.SellerData)
                   .Where(p => p.SellerData.isBanned == false)
                   .Where(p => !p.IsDeleted)
                   .Where(p => !p.IsArchived)
                   .Select(p => p.FromProduct_ToProductListItemVm())
                   .ToListAsync(),
            };
            ProductDetailVm vm2 = new ProductDetailVm
            {
                ProductListItems = await _db.Products
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages)
                    .Include(p => p.Tags)
                    .Include(p => p.SellerData)
                    .Where(p => p.SellerData.isBanned == false)
                    .Where(p => !p.IsDeleted)
                    .Where(p => !p.IsArchived)
                    .Select(p => p.FromProduct_ToProductListItemVm())
                    .ToListAsync(),
            };
            var combinedViewModel = new CombinedvmModel
            {
                HomeVm = vm,
                ProductDetailVm = vm2
            };

            // Pass the combinedViewModel to the view
            return View(combinedViewModel);
        }
    }
}
