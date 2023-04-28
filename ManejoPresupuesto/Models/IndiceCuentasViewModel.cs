

namespace ManejoPresupuesto.Models
{
    public class IndiceCuentasViewModel
    {
        public IEnumerable<Cuenta> Cuentas { get; set; }
        public decimal Balance => Cuentas.Sum( x => x.Balance);
    }
}