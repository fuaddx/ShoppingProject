using Microsoft.AspNetCore.Mvc;
using ShoppingMvc.Models;
using System.Diagnostics;

namespace ShoppingMvc.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
