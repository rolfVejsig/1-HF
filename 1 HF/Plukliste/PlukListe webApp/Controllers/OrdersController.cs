using Microsoft.AspNetCore.Mvc;

namespace PlukListe_webApp.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
