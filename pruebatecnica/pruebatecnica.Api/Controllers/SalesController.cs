using Microsoft.AspNetCore.Mvc;

namespace pruebatecnica.Api.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
