using ApiPeliculas.Models;
using ApiPeliculas.Models.Dtos;
using ApiPeliculas.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculaController : ControllerBase
    {
        private readonly IPeliculaRepositorio _pelRepo;
        private readonly IMapper _mapper;
        public PeliculaController(IPeliculaRepositorio peliculaRepositorio, IMapper mapper)
        {
            _pelRepo = peliculaRepositorio;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPeliculas()
        {
            var listaPeliculas = _pelRepo.GetPeliculas();
            var listaPeliculasDto = new List<PeliculaDto>();
            foreach (var pelicula in listaPeliculas)
            {
                listaPeliculasDto.Add(_mapper.Map<PeliculaDto>(pelicula));
            }
            return Ok(listaPeliculasDto);
        }
        [HttpGet("peliculaId:int", Name = "GetPelicula")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPelicula(int peliculaId)
        {
            var pelicula = _pelRepo.GetPelicula(peliculaId);
            if (pelicula == null)
            {
                return NotFound();
            }
            PeliculaDto peliculaDto = _mapper.Map<PeliculaDto>(pelicula);
            return Ok(peliculaDto);
        }
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PeliculaDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearPelicula([FromBody] PeliculaDto peliculaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (peliculaDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_pelRepo.ExistePelicula(peliculaDto.Nombre))
            {
                ModelState.AddModelError("", "La película ya existe");
                return StatusCode(404, ModelState);
            }
            Pelicula pelicula = _mapper.Map<Pelicula>(peliculaDto);

            if (!_pelRepo.CrearPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetPelicula", new { peliculaId = pelicula.Id }, pelicula);
        }
        [HttpPatch("peliculaId:int", Name = "ActualizarPatchPelicula")]
        [ProducesResponseType(201, Type = typeof(PeliculaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult ActualizarPatchPelicula(int peliculaId, [FromBody] PeliculaDto peliculaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (peliculaDto == null || peliculaId != peliculaDto.Id)
            {
                return BadRequest(ModelState);
            }

            Pelicula pelicula = _mapper.Map<Pelicula>(peliculaDto);
            if (!_pelRepo.ActualizarPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro: {pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("peliculaId:int", Name = "BorrarPelicula")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorrarPelicula(int peliculaId)
        {
            if (!_pelRepo.ExistePelicula(peliculaId))
            {
                return NotFound();
            }
            Pelicula pelicula = _pelRepo.GetPelicula(peliculaId);

            if (!_pelRepo.BorrarPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salió mal eliminando el registro{pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpGet("GetPeliculasEnCategoria/{categoriaId:int}")]
        public IActionResult GetPeliculasEnCategoria(int categoriaId)
        {
            var listaPeliculas = _pelRepo.GetPeliculasEnCategoria(categoriaId);
            if(listaPeliculas == null){
                return NotFound();
            }
            var listaPeliculasDto = new List<PeliculaDto>();
            foreach (var pelicula in listaPeliculas)
            {
                listaPeliculasDto.Add(_mapper.Map<PeliculaDto>(pelicula));
            }
            return Ok(listaPeliculasDto);
        }
    }
}