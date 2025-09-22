using AppPrueba.Models.DBInventario;
using Microsoft.AspNetCore.Mvc;

namespace AppPrueba.Controllers
{
    public class MarcasController : Controller
    {
        private readonly InventarioSistemasContext _context;


        public MarcasController(InventarioSistemasContext context)
        {
            _context = context;
        }


        public IActionResult Marcas()
        {
            var Marcas = _context.Marcas.ToList();

            return View(Marcas);
        }
    }
}
