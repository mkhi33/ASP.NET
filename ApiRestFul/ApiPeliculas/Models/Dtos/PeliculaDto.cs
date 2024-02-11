using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Models.Dtos
{
    public class PeliculaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }
        public string RutaImagen { get; set; }
        [Required(ErrorMessage = "La descripción es obligatorio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La duración es obligatorio")]
        public int Duracion { get; set; }
        public enum TipoClasificacion { Siete, Trece, Dieciseis, Diecioco }
        public TipoClasificacion clasificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int categoriaId { get; set; }
    }
}