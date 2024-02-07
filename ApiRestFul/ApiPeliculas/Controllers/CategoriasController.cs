
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    // [Route("api/[controller]")] Es una opción, pero si el nombre del controlador cambia entonces en endpoint cambia.
    [Route("api/categorias")] // El Endpoint se mantiene estatico.
    public class CategoriasController : ControllerBase
    {
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(ILogger<CategoriasController> logger)
        {
            _logger = logger;
        }

    }
}