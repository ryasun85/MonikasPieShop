using Microsoft.AspNetCore.Mvc;

namespace MonikasPieShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
