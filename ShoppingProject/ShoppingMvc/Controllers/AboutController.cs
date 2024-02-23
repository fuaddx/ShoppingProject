using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingMvc.Contexts;
using ShoppingMvc.ViewModels.AboutPageVm;
using ShoppingMvc.ViewModels.AboutVm;
using ShoppingMvc.ViewModels.BranchesVm;
using ShoppingMvc.ViewModels.ClientsVm;
using ShoppingMvc.ViewModels.ExpertsVm;
using ShoppingMvc.ViewModels.HomeVms;
using ShoppingMvc.ViewModels.SliderVm;
using System.Net;

namespace ShoppingMvc.Controllers
{
    public class AboutController : Controller
    {
        EvaraDbContext _db { get; set; }

        public AboutController(EvaraDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            AboutPageVIewModel vm = new AboutPageVIewModel()
            {
                AboutListItemvms = await _db.Aboutt.Select(c => new AboutListItemVm
                {
                    Id = c.Id,
                    IsArchived = c.IsArchived,
                    IsDeleted = c.IsDeleted,
                    Description = c.Description,
                    CreatedTime = c.CreatedTime,
                    UpdatedTime = c.UpdatedTime,
                    ImageUrl = c.ImageUrl,
                    Header = c.Header,
                    Title = c.Title,

                }).ToListAsync(),
				ExpertListItems = await _db.Expertss.Select(c => new ExpertListItemVm
				{
					Id = c.Id,
					CreatedTime = c.CreatedTime,
					UpdatedTime = c.UpdatedTime,
					ImageUrl = c.ImageUrl,
					IsDeleted = c.IsDeleted,
					IsArchived = c.IsArchived,
					Profession = c.Profession,
					Name = c.Name,

				}).ToListAsync(),
				BranchListItems = await _db.Brachess.Select(c => new BranchListItemVm
				{
					Id = c.Id,
					CreatedTime = c.CreatedTime,
					UpdatedTime = c.UpdatedTime,
					Place = c.Place,
					IsDeleted = c.IsDeleted,
					IsArchived = c.IsArchived,
					Address = c.Address,
					ImageUrl = c.ImageUrl

				}).ToListAsync(),
				ClientListItems = await _db.Client.Select(c => new ClientListItemVm
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

				}).ToListAsync(),
			};
            return View(vm);
        }
    }
}

                
