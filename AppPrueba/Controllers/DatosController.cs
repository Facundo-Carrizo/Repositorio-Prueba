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

        // LISTADO
        public IActionResult Index()
        {
            var datos = _contextInventario.Hardwares
                            .Where(h => h.IdModelo >= 1)
                            .ToList();

            return View(datos);
        }

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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHardware,Descripcion,IdModelo,NroSerie,Estado,FechaAlta,FechaBaja")] Hardware hardware)
        {
            if (id != hardware.IdHardware)
            {
                return NotFound();
            }

            //if (!ModelState.IsValid)
            //{
            //    // Log de errores para debug
            //    var errores = ModelState.Values
            //                             .SelectMany(v => v.Errors)
            //                             .Select(e => e.ErrorMessage)
            //                             .ToList();
            //    Console.WriteLine("❌ Errores en ModelState:");
            //    errores.ForEach(e => Console.WriteLine(e));

            //    return View(hardware);
            //}

            try
            {
                var hardwareDb = await _contextInventario.Hardwares.FindAsync(id);

                if (hardwareDb == null)
                    return NotFound();

                // Actualizamos solo los campos editables
                hardwareDb.Descripcion = hardware.Descripcion;
                hardwareDb.NroSerie = hardware.NroSerie;
                hardwareDb.Estado = hardware.Estado;
                hardwareDb.IdModelo = hardware.IdModelo;
                hardwareDb.FechaAlta = hardware.FechaAlta;
                hardwareDb.FechaBaja = hardware.FechaBaja;

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

            // ✅ Si todo salió bien, redirige al Index
            return RedirectToAction(nameof(Index));
        }

        private bool HardwareExists(int id)
        {
            return _contextInventario.Hardwares.Any(e => e.IdHardware == id);
        }
    }
}