
using ManejoPresupuesto.Interfaces;
using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc;


namespace ManejoPresupuesto.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly IRepositorioCategorias repositorioCategorias;
        private readonly IServicioUsuarios servicioUsuarios;

        public CategoriasController(IRepositorioCategorias repositorioCategorias, IServicioUsuarios servicioUsuarios)
        {
            this.repositorioCategorias = repositorioCategorias;
            this.servicioUsuarios = servicioUsuarios;
        }

        public async Task<IActionResult> Index(PaginacionViewModel paginacionViewModel)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var categorias = await repositorioCategorias.Obtener(usuarioId, paginacionViewModel);
            var totalCategorias = await repositorioCategorias.Contar(usuarioId);
            var respuetaViewModel = new PaginacionRespuesta<Categoria>
            {
                Elementos = categorias,
                Pagina = paginacionViewModel.Pagina,
                RecordsPorPagina = paginacionViewModel.RecordsPorPagina,
                CantidadTotalRecords = totalCategorias,
                BaseURL = Url.Action()
            };
            return View(respuetaViewModel);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            categoria.UsuarioId = usuarioId;
            await repositorioCategorias.Crear(categoria);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var categoria = await repositorioCategorias.ObtenerPorId(id, usuarioId);
            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var categoriaDb = await repositorioCategorias.ObtenerPorId(categoria.Id, usuarioId);
            if (categoriaDb is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            categoria.UsuarioId = usuarioId;
            await repositorioCategorias.Actualizar(categoria);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var categoria = await repositorioCategorias.ObtenerPorId(id, usuarioId);
            if(categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarCategoria(int id){
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var categoria = await repositorioCategorias.ObtenerPorId(id, usuarioId);
            if(categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            await repositorioCategorias.Eliminar(id, usuarioId);
            return RedirectToAction("Index");
        }

    }
}