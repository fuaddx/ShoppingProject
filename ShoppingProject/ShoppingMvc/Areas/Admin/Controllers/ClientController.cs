using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingMvc.Contexts;
using ShoppingMvc.Models;
using ShoppingMvc.ViewModels.ClientsVm;
using ShoppingMvc.ViewModels.CommonVm;

namespace ShoppingMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClientController : Controller
    {
        EvaraDbContext _db { get; set; }
        IWebHostEnvironment _env { get; set; }

        public ClientController(EvaraDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> ClientPagination(int page = 1, int count = 4)
        {
            var items = await _db.Client.Skip((page - 1) * count).Take(count).Select(c => new ClientListItemVm
            {
                Id = c.Id,
                CreatedTime = c.CreatedTime,
                UpdatedTime = c.UpdatedTime,
                Profession = c.Profession,
                IsDeleted = c.IsDeleted,
                IsArchived = c.IsArchived,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl
            }).ToListAsync();
            int totalCount = await _db.Client.CountAsync();
            PaginationVm<IEnumerable<ClientListItemVm>> pag = new(totalCount, page, (int)Math.Ceiling((decimal)totalCount / count), items);
            return PartialView("ClientPagination", pag);
        }

        public async Task<IActionResult> Index(string categoryFilter, DateTime? dateFilter, string statusFilter, int page = 1)
        {
            int take = 4;
            int skip = (page - 1) * take;

            var query = _db.Client.Select(c => new ClientListItemVm
            {
                Id = c.Id,
                CreatedTime = c.CreatedTime,
                UpdatedTime = c.UpdatedTime,
                Profession = c.Profession,
                IsDeleted = c.IsDeleted,
                IsArchived = c.IsArchived,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl
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



            PaginationVm<IEnumerable<ClientListItemVm>> pag = new PaginationVm<IEnumerable<ClientListItemVm>>(total, page, (int)Math.Ceiling((decimal)total / take), paginatedData);

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
            return Redirect("/Admin/Client/Index");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateVm vm)
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

            Clients client = new Clients()
            {
               Name = vm.Name,
               Profession = vm.Profession,
                Description = vm.Description,
                ImageUrl = filename,
            };
            _db.Client.AddAsync(client);
            await _db.SaveChangesAsync();
            TempData["Create"] = true;
            return Redirect("/Admin/Client/Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Client.FindAsync(id);
            if (data == null) return NotFound();
            return View(new ClientUpdateVm
            {
                Id = data.Id,
                Profession = data.Profession,
                Name = data.Name,
                Description = data.Description,
                ImageUrl = data.ImageUrl,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, ClientUpdateVm vm)
        {
            TempData["Update"] = false;
            if (id == null || id < 0) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }


            var data = await _db.Client.FindAsync(id);
            if (data == null) return NotFound();
            data.Id = vm.Id;
            data.Description = vm.Description;
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
            return Redirect("/Admin/Client/Index"); ;
        }
        public async Task<IActionResult> DeleteProduct(int? id)
        {

            if (id == null) return BadRequest();
            var data = await _db.Client.FindAsync(id);
            if (data == null) return NotFound();

            data.IsDeleted = true;
            await _db.SaveChangesAsync();
            return Redirect("/Admin/Client/Index");
        }
        public async Task<IActionResult> RestoreProduct(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Client.FindAsync(id);
            if (data == null) return NotFound();
            data.IsDeleted = false;
            await _db.SaveChangesAsync();
            return Redirect("/Admin/Client/Index");
        }
        public async Task<IActionResult> RestoreArchiveProduct(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Client.FindAsync(id);
            if (data == null) return NotFound();
            data.IsArchived = false;
            await _db.SaveChangesAsync();
            return Redirect("/Admin/Client/Index");
        }
        public async Task<IActionResult> DeleteFromData(int? id)
        {
            TempData["Delete"] = false;
            if (id == null) return BadRequest();
            var data = await _db.Client.FindAsync(id);
            if (data == null) return NotFound();
            TempData["Delete"] = true;
            _db.Client.Remove(data);
            await _db.SaveChangesAsync();
            return Redirect("/Admin/Client/Index");
        }
        public async Task<IActionResult> Archived(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Client.FindAsync(id);
            if (data == null) return NotFound();
            data.IsArchived = true;
            await _db.SaveChangesAsync();
            return Redirect("/Admin/Client/Index");
        }
    }
}
