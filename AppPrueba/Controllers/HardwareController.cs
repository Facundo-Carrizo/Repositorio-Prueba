using Microsoft.AspNetCore.Mvc;
using System;

namespace InventarioSistema.Controllers
{
    public class HardwareController : Controller
    {
        public IActionResult Index()
        {
        //    //List <Hardware> lst = new List<Hardware>();
        //    //using (var db = new Models.DBInventario.InventarioSistemasContext())
        //    //{
        //    //    lst = (from d in db.Hardwares
        //    //           select new Hardware
        //    //           {
        //    //               id_hardware = d.id_hardware,
        //    //               id_modelo = d.id_modelo,
        //    //               estado = d.estado
        //    //           }).ToList();
        //    //}
            //return View(lst);
            return View();
        }
    }
}
