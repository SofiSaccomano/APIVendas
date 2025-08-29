using Microsoft.AspNetCore.Mvc;

namespace APIVendas.Controllers
{
    public class PedidosProdutosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
