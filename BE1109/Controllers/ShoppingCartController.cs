using Microsoft.AspNetCore.Mvc;

namespace BE1109.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
