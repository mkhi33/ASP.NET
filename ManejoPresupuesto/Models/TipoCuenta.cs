
using System.ComponentModel.DataAnnotations;
using ManejoPresupuesto.Validaciones;

namespace ManejoPresupuesto.Models
{
    public class TipoCuenta
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        public int Orden { get; set; }

        // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        // {
        //     if (Nombre != null && Nombre.Length > 0)
        //     {
        //         var primeraLetra = Nombre.ToString()[0].ToString();
        //         if (primeraLetra != primeraLetra.ToUpper())
        //         {
        //             yield return new ValidationResult("La primera letra debe ser mayúscula", new string[] { nameof(Nombre) });
        //         }
        //     }
        // }
    }
}