using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingMvc.Contexts;
using ShoppingMvc.Models;
using ShoppingMvc.Models.Identity;
using ShoppingMvc.ViewModels.CommonVm;
using ShoppingMvc.ViewModels.SellerVm;
using ShoppingMvc.ViewModels.UserVm;

namespace ShoppingMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Moderator")]
    public class UserController : Controller
    {
        private readonly EvaraDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly int _take = 8;

        public UserController(EvaraDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _userManager = userManager;
        }
       

        public IActionResult Index()
        {
            var users = _userManager.Users;
            return View(users);
        }
    }
}
