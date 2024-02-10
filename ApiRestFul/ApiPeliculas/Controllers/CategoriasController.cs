
using ApiPeliculas.Models.Dtos;
using ApiPeliculas.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    // [Route("api/[controller]")] Es una opción, pero si el nombre del controlador cambia entonces en endpoint cambia.
    [Route("api/categorias")] // El Endpoint se mantiene estatico.
    public class CategoriasController : ControllerBase
    {
        private readonly ILogger<CategoriasController> _logger;
        private readonly ICategoriaRepositorio _ctRepo;
        private readonly IMapper _mapper;

        public CategoriasController(ILogger<CategoriasController> logger, ICategoriaRepositorio ctRepo, IMapper mapper)
        {
            _logger = logger;
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _ctRepo.GetCategorias();
            var listaCategoriasDto = new List<CategoriaDto>();
            foreach (var categoria in listaCategorias)
            {
                listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(categoria));
            }
            return Ok(listaCategoriasDto);
        }

        [HttpGet("categoriaId:int", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoria(int categoriaId)
        {
            var categoria = _ctRepo.GetCategoria(categoriaId);
            if (categoria == null)
            {
                return NotFound();
            }
            CategoriaDto categoriaDto = _mapper.Map<CategoriaDto>(categoria);
            return Ok(categoria);
        }


    }
}