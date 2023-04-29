using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManejoPresupuesto.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:50, ErrorMessage = "El nombre de la categoría no puede tener más de {1} caracteres")]
        public string Nombre { get; set; }
        public TipoOperacion TipoOperacionId { get; set; }
        public int UsuarioId { get; set; }
        
    }
}