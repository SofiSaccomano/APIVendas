using Microsoft.AspNetCore.Mvc;

namespace APIVendas.Controllers
{
    public class PedidosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
