using Microsoft.AspNetCore.Mvc;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController : Controller
    {
        private readonly string connectionString;
        private readonly ILogger<HomeController> _logger;

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