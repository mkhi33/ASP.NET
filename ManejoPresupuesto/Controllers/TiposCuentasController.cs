using Microsoft.AspNetCore.Mvc;
using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(TipoCuenta tipoCuenta )
        {
            if( !ModelState.IsValid )
            {
                return View(tipoCuenta);
            }
            return View();
        }
    }
}