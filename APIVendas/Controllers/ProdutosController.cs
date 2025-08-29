using Microsoft.AspNetCore.Mvc;

namespace APIVendas.Controllers
{
    public class ProdutosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
