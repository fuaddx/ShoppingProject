using Microsoft.AspNetCore.Mvc;

namespace ShoppingMvc.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
