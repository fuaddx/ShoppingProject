﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using ShoppingMvc.Contexts;
using ShoppingMvc.Models;
using ShoppingMvc.ViewModels.CommonVm;
using ShoppingMvc.ViewModels.ExpertsVm;

namespace ShoppingMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
        public class ExpertController : Controller
        {
            EvaraDbContext _db { get; set; }
            IWebHostEnvironment _env { get; set; }

            public ExpertController(EvaraDbContext db, IWebHostEnvironment env)
            {
                _db = db;
                _env = env;
            }
        
        public async Task<IActionResult> ExpertPagination(int page = 1, int count = 4)
            {
                var items = await _db.Expertss.Skip((page - 1) * count).Take(count).Select(c => new ExpertListItemVm
                {
                    Id = c.Id,
                    CreatedTime = c.CreatedTime,
                    UpdatedTime = c.UpdatedTime,
                    ImageUrl = c.ImageUrl,
                    IsDeleted = c.IsDeleted,
                    IsArchived = c.IsArchived,
                    Profession = c.Profession,
                    Name = c.Name,
                }).ToListAsync();
            int totalCount = await _db.Expertss.CountAsync();
                PaginationVm<IEnumerable<ExpertListItemVm>> pag = new(totalCount, page, (int)Math.Ceiling((decimal)totalCount / count), items);
                return PartialView("ExpertPagination", pag);
            }

            public async Task<IActionResult> Index(string categoryFilter, DateTime? dateFilter, string statusFilter, int page = 1)
            {
                int take = 4;
                int skip = (page - 1) * take;

                var query = _db.Expertss.Select(c => new ExpertListItemVm
                {
                    Id = c.Id,
                    CreatedTime = c.CreatedTime,
                    UpdatedTime = c.UpdatedTime,
                    ImageUrl = c.ImageUrl,
                    IsDeleted = c.IsDeleted,
                    IsArchived = c.IsArchived,
                    Profession = c.Profession,
                    Name = c.Name,
                });

               
                query = query.OrderBy(c => c.CreatedTime);

                var results = query.ToList();

                if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "All statuses" && statusFilter != "Show all")
                {
                    query = query.Where(c => c.IsDeleted && statusFilter == "Disabled" || !c.IsDeleted && statusFilter == "Active");
                }

                var filteredData = await query.ToListAsync();

                int total = filteredData.Count();

                var paginatedData = filteredData.Skip(skip).Take(take).ToList();
                query = query.OrderBy(c => c.CreatedTime);



                PaginationVm<IEnumerable<ExpertListItemVm>> pag = new PaginationVm<IEnumerable<ExpertListItemVm>>(total, page, (int)Math.Ceiling((decimal)total / take), paginatedData);

                if (paginatedData.Count == 0)
                {
                    ViewBag.Message = "Nothing Found";
                }

                return View(pag);
            }

            public async Task<IActionResult> Create()
            {
                return View();
            }
            public async Task<IActionResult> Cancel()
            {
                return Redirect("/Admin/Expert/Index");
            }
            [HttpPost]
            public async Task<IActionResult> Create(ExpertCreateListItemVm vm)
            {
                if (!ModelState.IsValid)
                {
                    return View();

                }
                string filename = null;
                if (vm.MainImage != null)
                {
                    filename = Guid.NewGuid() + Path.GetExtension(vm.MainImage.FileName);
                    using (Stream fs = new FileStream(Path.Combine(_env.WebRootPath, "Assets", "assets", "products", filename), FileMode.Create))
                    {
                        await vm.MainImage.CopyToAsync(fs);
                    }
                }
                Experts slider = new Experts()
                {
                    Name = vm.Name,
                    Profession = vm.Profession,
                    ImageUrl = filename,
                };
                _db.Expertss.AddAsync(slider);
                await _db.SaveChangesAsync();
                TempData["Create"] = true;
                return Redirect("/Admin/Expert/Index");
            }
            public async Task<IActionResult> Update(int? id)
            {
                if (id == null) return BadRequest();
                var data = await _db.Expertss.FindAsync(id);
                if (data == null) return NotFound();
                return View(new ExpertUpdateVm
                {
                    Id = data.Id,
                    ImageUrl = data.ImageUrl,
                    Name = data.Name,
                    Profession = data.Profession,
                });
            }
            [HttpPost]
            public async Task<IActionResult> Update(int? id, ExpertUpdateVm vm)
            {
                TempData["Update"] = false;
                if (id == null || id < 0) return BadRequest();
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
               

                var data = await _db.Expertss.FindAsync(id);
                if (data == null) return NotFound();
                data.Id = vm.Id;
                data.Name = vm.Name;
                data.Profession = vm.Profession;
                if (!string.IsNullOrEmpty(data.ImageUrl))
                {
                    string filepath = Path.Combine(_env.WebRootPath, "Assets", "assets", "products", data.ImageUrl);
                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                }
                string filename = Guid.NewGuid() + Path.GetExtension(vm.MainImage.FileName);
                using (Stream fs = new FileStream(Path.Combine(_env.WebRootPath, "Assets", "assets", "products", filename), FileMode.Create))
                {
                    await vm.MainImage.CopyToAsync(fs);
                }
                data.ImageUrl = filename;
                await _db.SaveChangesAsync();
                TempData["Update"] = true;
                return Redirect("/Admin/Expert/Index"); ;
            }
        public async Task<IActionResult> DeleteProduct(int? id)
        {

            if (id == null) return BadRequest();
            var data = await _db.Expertss.FindAsync(id);
            if (data == null) return NotFound();

            data.IsDeleted = true;
            await _db.SaveChangesAsync();
            return Redirect("/Admin/Expert/Index");
        }
        public async Task<IActionResult> RestoreProduct(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Expertss.FindAsync(id);
            if (data == null) return NotFound();
            data.IsDeleted = false;
            await _db.SaveChangesAsync();
            return Redirect("/Admin/Expert/Index");
        }
        public async Task<IActionResult> RestoreArchiveProduct(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Expertss.FindAsync(id);
            if (data == null) return NotFound();
            data.IsArchived = false;
            await _db.SaveChangesAsync();
            return Redirect("/Admin/Expert/Index");
        }
        public async Task<IActionResult> DeleteFromData(int? id)
        {
            TempData["Delete"] = false;
            if (id == null) return BadRequest();
            var data = await _db.Expertss.FindAsync(id);
            if (data == null) return NotFound();
            TempData["Delete"] = true;
            _db.Expertss.Remove(data);
            await _db.SaveChangesAsync();
            return Redirect("/Admin/Expert/Index");
        }
        public async Task<IActionResult> Archived(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Expertss.FindAsync(id);
            if (data == null) return NotFound();
            data.IsArchived = true;
            await _db.SaveChangesAsync();
            return Redirect("/Admin/Expert/Index");
        }
    }
    
}
