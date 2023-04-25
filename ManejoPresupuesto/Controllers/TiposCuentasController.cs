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
        public TiposCuentasController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }
        public IActionResult Crear()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = connection.Query("SELECT 1").FirstOrDefault();
                _logger.LogInformation($"Query: {query}");
            }
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