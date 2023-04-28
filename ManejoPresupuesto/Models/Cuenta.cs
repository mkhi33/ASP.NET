
using System.ComponentModel.DataAnnotations;
using ManejoPresupuesto.Models;
using ManejoPresupuesto.Validaciones;

namespace ManejoPresupuesto.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        [Required(ErrorMessage="El campo {0} es requerido")]
        [StringLength(50, MinimumLength=3, ErrorMessage="El campo {0} debe tener entre {2} y {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        [Display(Name="Tipo de Cuenta")]
        public int TipoCuentaId { get; set; }
        public decimal Balance { get; set; }
        [StringLength(maximumLength:1000)]
        public string Descripcion { get; set; }

    }
}