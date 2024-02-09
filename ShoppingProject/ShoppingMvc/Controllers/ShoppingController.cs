using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShoppingMvc.Contexts;
using ShoppingMvc.ViewModels.BasketVm;
using ShoppingMvc.ViewModels.CategoryVm;
using ShoppingMvc.ViewModels.CommonVm;
using ShoppingMvc.ViewModels.HomeVm;
using ShoppingMvc.ViewModels.ProductVm;
using ShoppingMvc.ViewModels.SliderVm;

namespace ShoppingMvc.Controllers
{
    public class ShoppingController : Controller
    {
        EvaraDbContext _db { get; set; }

        public ShoppingController(EvaraDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            int take = 7;
            var items = _db.Products.Take(take).Select(c => new ProductListItemVm
            {
                Id = c.Id,
                CreatedTime = c.CreatedTime,
                UpdatedTime = c.UpdatedTime,
                ImageUrl = c.ImageUrl,
                IsDeleted = c.IsDeleted,
                IsArchived = c.IsArchived,
                Title = c.Title,
                Description = c.Description,
                CostPrice = c.CostPrice,
                SellPrice = c.SellPrice,
                Category = c.Category,
                RateRange = c.RateRange,
                Tags = c.TagProduct.Select(p => p.Tag)
            });
            int count = await _db.Products.CountAsync();
            PaginationVm<IEnumerable<ProductListItemVm>> pag = new(count, 1, (int)Math.Ceiling((decimal)count / take), items);
            HomeVm vm = new HomeVm
            {
                ProductListItems = await _db.Products.Select(c => new ProductListItemVm
                {
                    Id = c.Id,
                    CreatedTime = c.CreatedTime,
                    UpdatedTime = c.UpdatedTime,
                    ImageUrl = c.ImageUrl,
                    IsDeleted = c.IsDeleted,
                    IsArchived = c.IsArchived,
                    Title = c.Title,
                    Description = c.Description,
                    CostPrice = c.CostPrice,
                    SellPrice = c.SellPrice,
                    Category = c.Category,
                    RateRange = c.RateRange,
                    Tags = c.TagProduct.Select(p => p.Tag)
                }).ToListAsync(),
                CategoryListItemVms = await _db.Categorys.Select(c => new CategoryListItemVm
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync(),
                PaginationProduct = pag
            };
            return View(vm);
            
        }
        public async Task<IActionResult> ProductPagination(int page = 1, int count = 8)
        {
            if (count == 1)
            {
                // If count is 1, set page to 1 to ensure only one item is shown
                page = 1;
            }
            var items = _db.Products.Skip((page - 1) * count).Take(count).Select(c => new ProductListItemVm
            {
                Id = c.Id,
                CreatedTime = c.CreatedTime,
                UpdatedTime = c.UpdatedTime,
                ImageUrl = c.ImageUrl,
                IsDeleted = c.IsDeleted,
                IsArchived = c.IsArchived,
                Title = c.Title,
                Description = c.Description,
                CostPrice = c.CostPrice,
                SellPrice = c.SellPrice,
                Category = c.Category,
                RateRange = c.RateRange,
                Tags = c.TagProduct.Select(p => p.Tag)
            });
            int totalCount = await _db.Products.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCount / count);
            if (page > totalPages)
            {
                page = totalPages;
            }
            PaginationVm<IEnumerable<ProductListItemVm>> pag = new(totalCount, page,totalPages, items);

            return PartialView("ProductPagination", pag);
        }
        
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            if (!await _db.Products.AnyAsync(p => p.Id == id)) return NotFound();
            var basket = JsonConvert.DeserializeObject<List<BasketProductandCountVm>>(HttpContext.Request.Cookies["basket"] ?? "[]");
            var existItem = basket.Find(p => p.Id == id);
            if (existItem == null)
            {
                basket.Add(new BasketProductandCountVm
                {
                    Id = (int)id,
                    Count = 1

                });
            }
            else
            {
                existItem.Count++;
            }
            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            return Ok();
        }
        public async Task<IActionResult> RemoveBasket(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            if (!await _db.Products.AnyAsync(p => p.Id == id)) return NotFound();
            var dasket = JsonConvert.DeserializeObject<List<BasketProductandCountVm>>(HttpContext.Request.Cookies["basket"] ?? "[]");
            var existItem = dasket.Find(b => b.Id == id);
            if (existItem.Count == 1)
            {
                dasket.Remove(new BasketProductandCountVm()
                {
                    Id = (int)id,
                    Count = 1
                });
            }
            else
            {
                existItem.Count--;
            }
            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(dasket));
            return Ok();
        }

    }
}
