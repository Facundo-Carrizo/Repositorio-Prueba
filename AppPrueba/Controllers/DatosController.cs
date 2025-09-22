using AppPrueba.Models.DBInventario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppPrueba.Controllers
{
    public class DatosController : Controller
    {
        private readonly InventarioSistemasContext _contextInventario;

        public DatosController(InventarioSistemasContext context)
        {
            _contextInventario = context;
        }

        public IActionResult Index()
        {
            var datos = _contextInventario.Hardwares
                            .Where(h => h.IdModelo >= 5)
                            .ToList();

            return View(datos);
        }
        //  18/9/25: Implemetamos el sigjuiente codigo para poder editar los datos de los dispositivos tambien creamos una vista llamada Edit.cs Ubicada en Views/Datos
        // Actualmente no esta fucionando el update al querer editar un dispositivo, se debe revisar el codigo de  GET: Datos/Edit/5

        // GET: Datos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hardware = await _contextInventario.Hardwares.FindAsync(id);
            if (hardware == null)
            {
                return NotFound();
            }

            return View(hardware);
        }

        // POST: Datos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHardware,Descripcion,IdModelo,NroSerie,Estado")] Hardware hardware)
        {
            if (id != hardware.IdHardware)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contextInventario.Update(hardware);
                    await _contextInventario.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HardwareExists(hardware.IdHardware))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hardware);
        }

        private bool HardwareExists(int id)
        {
            return _contextInventario.Hardwares.Any(e => e.IdHardware == id);
        }
    }
}