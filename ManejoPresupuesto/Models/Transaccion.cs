
using System.ComponentModel.DataAnnotations;
namespace ManejoPresupuesto.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        [Display(Name = "Fecha de Transacción")]
        [DataType(DataType.Date)]
        public DateTime FechaTransaccion { get; set; } = DateTime.Today;
        public decimal Monto { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una categoría")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }
        [StringLength(maximumLength: 1000, ErrorMessage = "La nota no puede tener más de {1} caracteres")]
        public string Nota { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una Cuenta")]
        [Display(Name = "Cuenta")]
        public int CuentaId { get; set; }
    }
}